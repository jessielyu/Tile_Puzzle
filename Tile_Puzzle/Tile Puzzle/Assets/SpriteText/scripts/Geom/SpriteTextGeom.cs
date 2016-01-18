//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
//#define ENABLE_GEOM_PROFILER
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

[Flags]
internal enum SpriteTextFlags
{
	UpdateNone = 0,
	UpdateGeom = 1,
	UpdateColor = 2,
	UpdateBuffer = 4,
}

internal class SpriteTextGeom
{
	public SpriteTextGeom(SpriteText spriteText)
	{
		m_spriteText = spriteText;
	}

	public bool NeedUpdate
	{
		get { return m_flags != SpriteTextFlags.UpdateNone; }
	}

	public string DisplayedText
	{
		get { return m_displayedText; }
	}

	public void SetChanged(SpriteTextFlags flag)
	{
		m_flags |= flag;
	}

	public void Recalc(Mesh mesh)
	{
		if (NeedUpdateBuffer())
		{
			BeginSample("ApplyLength");
			ApplyLength();
			EndSample();
		}

		if (NeedUpdateInline())
		{
			BeginSample("ApplyInlineColor");
			ApplyInlineColor();
			EndSample();
		}
		else if(!m_spriteText.InlineColor)
		{
			m_displayedText = m_spriteText.Text;
		}

		if (NeedUpdateGeom())
		{
			BeginSample("ResetGeom");
			ResetGeom();
			EndSample();

			BeginSample("ParseText");
			ParseText();
			EndSample();

			BeginSample("ApplyPivot");
			ApplyPivot();
			EndSample();

			BeginSample("ApplyAlignment");
			ApplyAlignment();
			EndSample();
		}

		if (NeedUpdateColor())
		{
			BeginSample("ApplyColor");
			ApplyColor();
			EndSample();
		}

		if (NeedUpdateGeom())
		{
			BeginSample("ApplyScale");
			ApplyScale();
			EndSample();
		}

		BeginSample("Apply geom to mesh");
		if (NeedUpdateBuffer())
		{
			BeginSample("Clear mesh");
			mesh.Clear();
			EndSample();
		}
		if (NeedUpdateGeom())
		{
			BeginSample("Set vertices and uv");
			mesh.subMeshCount = m_triangles.Length;
			mesh.vertices = m_vertixes;
			mesh.uv = m_uv;
			for (int i = 0; i < m_triangles.Length; i++)
			{
				mesh.SetTriangles(m_triangles[i], i);
			}
			EndSample();
		}
		if (NeedUpdateColor())
		{
			BeginSample("Set colors");
			mesh.colors = m_colors;
			EndSample();
		}
		if (NeedUpdateBuffer())
		{
			BeginSample("Recalculate bounds and normals");
			mesh.RecalculateBounds();
			mesh.RecalculateNormals();
			EndSample();
		}
		EndSample();

		m_flags = SpriteTextFlags.UpdateNone;
	}

	private void ResetGeom()
	{
		m_index = 0;
		m_linesCount = 0;
		m_linesLength.Clear();
		m_linesWidth.Clear();
		for (int i = 0; i < m_spriteText.Capacity * 4; i++)
		{
			m_vertixes[i].Set(0.0f, 0.0f, 0.0f);
			m_uv[i].Set(0.0f, 0.0f);
		}
		for (int i = 0; i < m_triangles.Length; i++)
		{
			m_triangleIndex[i] = 0;
			for (int j = 0; j < m_triangles[i].Length; j++)
			{
				m_triangles[i][j] = 0;
			}
		}
		m_size.Set(0.0f, 0.0f);
	}

	private void ParseText()
	{
		string[] paragraphs = m_displayedText.Split('\n');
		float yoffset = 0.0f;

		for (int i = 0; i < paragraphs.Length; i++)
		{
			BeginSample("Parse paragraph");
			string paragraph = paragraphs[i];
			if (paragraph == "")
			{
				yoffset += m_spriteText.Font.FontData.Common.LineHeight + m_spriteText.Font.FontData.Info.Spacing.y;
			}
			else if (m_spriteText.LineWrap)
			{
				BeginSample("Parse text to lines");
				List<string> stringLines = SpriteTextParser.ParseText(m_spriteText, paragraph);
				EndSample();
				foreach (string stringLine in stringLines)
				{
					BeginSample("PushLine line");
					PushLine(stringLine, yoffset);
					EndSample();

					yoffset += m_spriteText.Font.FontData.Common.LineHeight + m_spriteText.Font.FontData.Info.Spacing.y;
					if (m_index >= m_spriteText.Capacity)
					{
						break;
					}
				}
			}
			else
			{
				BeginSample("PushLine line");
				PushLine(paragraph, yoffset);
				EndSample();
				yoffset += m_spriteText.Font.FontData.Common.LineHeight + m_spriteText.Font.FontData.Info.Spacing.y;
				if (m_index >= m_spriteText.Capacity)
				{
					break;
				}
			}
			EndSample();
		}
		m_size.y = yoffset;
		if (m_spriteText.LineWrap)
		{
			m_size.x = m_spriteText.LineWidth * m_spriteText.Font.PixelToUnits;
		}
	}

	private void ApplyInlineColor()
	{
		string text = m_spriteText.Text;
		StringBuilder display = new StringBuilder();

		int index = 0;

		for (int i = 0; i < text.Length; i++)
		{
			int end;
			if (text[i] == '[' && (end = text.IndexOf(']', i)) != -1)
			{
				int length = end - i;
				int colon = text.IndexOf(':', i, length);
				string hcolor = text.Substring(i + 1, colon - i - 1).ToLower();
				string[] hcolors = hcolor.Split(s_hexSplit, StringSplitOptions.RemoveEmptyEntries);
				if ((hcolors.Length == 1 || hcolors.Length == 2) && hcolors.All(IsHexColor))
				{
					Color c1 = HexToColor(hcolors[0]);
					Color c2 = c1;
					if (hcolors.Length == 2)
					{
						c2 = HexToColor(hcolors[1]);
					}
					c1.a *= (1.0f - m_spriteText.Transparency);
					c2.a *= (1.0f - m_spriteText.Transparency);
					string inlineText = text.Substring(colon + 1, length - hcolor.Length - 2);

					for (int j = 0; j < inlineText.Length; j++)
					{
						display.Append(inlineText[j]);
						if (inlineText[j] == '\n')
						{
							continue;
						}
						if (index < m_spriteText.Capacity)
						{
							m_inlineSymbols[index] = true;
							SetSymbolColor(index, ref c1, ref c2);
						}
						index++;
					}
					i = end;
					continue;
				}
			}
			display.Append(text[i]);
			if (text[i] != '\n')
			{
				if (index < m_spriteText.Capacity)
				{
					m_inlineSymbols[index] = false;
				}
				index++;
			}
		}
		m_displayedText = display.ToString();
	}

	private void ApplyLength()
	{
		int length = m_spriteText.Capacity;
		int pages = m_spriteText.Font.FontData.Common.Pages;
		m_vertixes = new Vector3[length * 4 * pages];
		m_uv = new Vector2[length * 4 * pages];
		m_colors = new Color[length * 4 * pages];
		m_inlineSymbols = new bool[length];
		m_triangles = new int[pages][];
		for (int i = 0; i < pages; i++)
		{
			m_triangles[i] = new int[length * 6];
		}
		m_triangleIndex = new int[pages];
	}

	private void ApplyAlignment()
	{
		int index = 0;
		for (int i = 0; i < m_linesCount; i++)
		{
			float xoffset = 0.0f;
			switch (m_spriteText.Alignment)
			{
				case TextAlignment.Left:
					break;
				case TextAlignment.Center:
					xoffset = (m_size.x - m_linesWidth[i]) / 2.0f;
					break;
				case TextAlignment.Right:
					xoffset = m_size.x - m_linesWidth[i];
					break;
			}
			for (int j = 0; j < m_linesLength[i] && index < m_spriteText.Capacity; j++)
			{
				int i4 = index * 4;
				m_vertixes[i4 + 0].x += xoffset;
				m_vertixes[i4 + 1].x += xoffset;
				m_vertixes[i4 + 2].x += xoffset;
				m_vertixes[i4 + 3].x += xoffset;
				index++;
			}
		}
	}

	private void ApplyPivot()
	{
		float yoffset = 0.0f;
		float xoffset = 0.0f;

		switch (m_spriteText.Pivot)
		{
			case TextAnchor.UpperRight:
			case TextAnchor.MiddleRight:
			case TextAnchor.LowerRight:
				xoffset = -m_size.x;
				break;
			case TextAnchor.UpperCenter:
			case TextAnchor.MiddleCenter:
			case TextAnchor.LowerCenter:
				xoffset = -m_size.x / 2.0f;
				break;
		}

		switch (m_spriteText.Pivot)
		{
			case TextAnchor.LowerLeft:
			case TextAnchor.LowerCenter:
			case TextAnchor.LowerRight:
				yoffset = m_size.y;
				break;
			case TextAnchor.MiddleLeft:
			case TextAnchor.MiddleCenter:
			case TextAnchor.MiddleRight:
				yoffset = m_size.y / 2.0f;
				break;
		}
		for (int i = 0; i < m_vertixes.Length; i++)
		{
			m_vertixes[i].x += xoffset;
			m_vertixes[i].y += yoffset;
		}
	}

	private void ApplyColor()
	{
		Color c1 = m_spriteText.Color1;
		Color c2 = m_spriteText.Gradient ? m_spriteText.Color2 : m_spriteText.Color1;

		c1.a *= (1.0f - m_spriteText.Transparency);
		c2.a *= (1.0f - m_spriteText.Transparency);

		for (int i = 0; i < m_spriteText.Capacity; i++)
		{
			if (!m_spriteText.InlineColor || !m_inlineSymbols[i])
			{
				SetSymbolColor(i, ref c1, ref c2);
			}
		}
	}

	private void ApplyScale()
	{
		for (int i = 0; i < m_vertixes.Length; i++)
		{
			m_vertixes[i] /= m_spriteText.Font.PixelToUnits;
			m_vertixes[i].x *= m_spriteText.Scale;
			m_vertixes[i].y *= m_spriteText.Scale;
		}
	}

	public void PushLine(string line, float yoffset)
	{
		if (line.Length == 0)
		{
			return;
		}
		float xoffset = 0;

		int end = 0;
		for (int i = line.Length - 1; i >= 0; i--)
		{
			if (line[i] != ' ')
			{
				end = i + 1;
				break;
			}
		}
		for (int i = 0; i < line.Length && m_index < m_spriteText.Capacity; i++)
		{
			int i4 = m_index * 4;

			if (i == end)
			{
				m_index++;
				break;
			}
			FontChar fontChar = m_spriteText.Font[line[i]];
			if (fontChar == null)
			{
				continue;
			}
			float xMin = fontChar.XOffset;
			float xMax = xMin + fontChar.Width;
			float yMin = -fontChar.Height - fontChar.YOffset - yoffset;
			float yMax = -fontChar.YOffset - yoffset;
			m_vertixes[i4 + 0].Set(xMin + xoffset, yMin, 0);
			m_vertixes[i4 + 1].Set(xMin + xoffset, yMax, 0);
			m_vertixes[i4 + 2].Set(xMax + xoffset, yMax, 0);
			m_vertixes[i4 + 3].Set(xMax + xoffset, yMin, 0);

			m_uv[i4 + 0].Set(fontChar.Uv.xMin, 1 - fontChar.Uv.yMax);
			m_uv[i4 + 1].Set(fontChar.Uv.xMin, 1 - fontChar.Uv.yMin);
			m_uv[i4 + 2].Set(fontChar.Uv.xMax, 1 - fontChar.Uv.yMin);
			m_uv[i4 + 3].Set(fontChar.Uv.xMax, 1 - fontChar.Uv.yMax);

			int triangeIndex = m_triangleIndex[fontChar.Page];
			m_triangles[fontChar.Page][0 + triangeIndex] = 0 + i4;
			m_triangles[fontChar.Page][1 + triangeIndex] = 1 + i4;
			m_triangles[fontChar.Page][2 + triangeIndex] = 2 + i4;
			m_triangles[fontChar.Page][3 + triangeIndex] = 0 + i4;
			m_triangles[fontChar.Page][4 + triangeIndex] = 2 + i4;
			m_triangles[fontChar.Page][5 + triangeIndex] = 3 + i4;
			m_triangleIndex[fontChar.Page] += 6;

			xoffset += fontChar.XAdvance + m_spriteText.Font.FontData.Info.Spacing.x;

			if (i < line.Length - 1)
			{
				FontKerning kerning = m_spriteText.Font[line[i]][m_spriteText.Font, line[i + 1]];
				if (kerning != null)
				{
					xoffset += kerning.Amount;
				}
			}
			m_index++;
		}
		m_linesWidth.Add(xoffset);
		m_linesLength.Add(line.Length);
		m_linesCount++;
		m_size.x = Mathf.Max(m_size.x, xoffset);
	}


	private bool NeedUpdateGeom()
	{
		return (m_flags & SpriteTextFlags.UpdateGeom) != 0 || NeedUpdateBuffer();
	}

	private bool NeedUpdateInline()
	{
		return NeedUpdateBuffer() && m_spriteText.InlineColor;
	}

	private bool NeedUpdateColor()
	{
		return (m_flags & SpriteTextFlags.UpdateColor) != 0 || NeedUpdateBuffer();
	}

	private bool NeedUpdateBuffer()
	{
		return (m_flags & SpriteTextFlags.UpdateBuffer) != 0;
	}

	private Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
		byte a = 255;
		if (hex.Length == 8)
		{
			a = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
		}
		return new Color32(r, g, b, a);
	}

	private bool IsHexColor(string color)
	{
		if (color.Length != 6 && color.Length != 8)
		{
			return false;
		}
		return color.All(IsHexDigit);
	}

	private bool IsHexDigit(char c)
	{
		return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f');
	}

	private void SetSymbolColor(int index, ref Color c1, ref Color c2)
	{
		if (index < m_spriteText.Capacity)
		{
			for (int i = 0; i < m_spriteText.Font.FontData.Common.Pages; i++)
			{
				int offset = index + m_spriteText.Capacity * i;
				int i4 = offset * 4;
				m_colors[i4 + 0] = c2;
				m_colors[i4 + 1] = c1;
				m_colors[i4 + 2] = c1;
				m_colors[i4 + 3] = c2;
			}
		}
	}

	[System.Diagnostics.Conditional("ENABLE_GEOM_PROFILER")]
	private void BeginSample(string sampleName)
	{
		Profiler.BeginSample(sampleName);
	}

	[System.Diagnostics.Conditional("ENABLE_GEOM_PROFILER")]
	private void EndSample()
	{
		Profiler.EndSample();
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	[System.Diagnostics.DebuggerStepThrough]
	private void LogVertixes()
	{
		string info = m_vertixes.Aggregate("Vertixes:", (current, vertix) => current + ("\n" + vertix));
		Debug.Log(info);
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	[System.Diagnostics.DebuggerStepThrough]
	private void LogUV()
	{
		string info = m_uv.Aggregate("UV:", (current, uv) => current + ("\n" + uv));
		Debug.Log(info);
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	[System.Diagnostics.DebuggerStepThrough]
	private void LogTriangles()
	{
		string info = "";
		for (int i = 0; i < m_triangles.Length; i++)
		{
			info += "Submesh [" + i + "]:\n";
			for (int j = 0; j < m_triangles[i].Length / 6; j += 6)
			{
				info += string.Format("[{0} {1} {2} {3} {4} {5}]\n",
					m_triangles[i][j + 0],
					m_triangles[i][j + 1],
					m_triangles[i][j + 2],
					m_triangles[i][j + 3],
					m_triangles[i][j + 4],
					m_triangles[i][j + 5]);
			}
		}
		Debug.Log(info);
	}

	private SpriteTextFlags m_flags = SpriteTextFlags.UpdateNone;

	private Vector3[] m_vertixes = new Vector3[0];
	private Vector2[] m_uv = new Vector2[0];
	private int[][] m_triangles = new int[0][];
	private Color[] m_colors = new Color[0];

	private bool[] m_inlineSymbols = new bool[0];

	private int m_index = 0;
	private int[] m_triangleIndex = new int[0];

	private List<int> m_linesLength = new List<int>();
	private List<float> m_linesWidth = new List<float>();
	private int m_linesCount = 0;

	private Vector2 m_size = new Vector2(0.0f, 0.0f);
	private SpriteText m_spriteText = null;
	private string m_displayedText = "";

	private static char[] s_hexSplit = { ' ' };
}