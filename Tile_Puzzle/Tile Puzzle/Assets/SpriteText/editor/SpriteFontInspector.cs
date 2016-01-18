//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteFont))]
public class SpriteFontInspector : Editor
{
	private void OnEnable()
	{
		m_font = (SpriteFont)target;
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		SerializedProperty iterator = serializedObject.GetIterator();
		for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
		{
			if (iterator.name == "m_fontData")
			{
				GUI.enabled = false;
			}
			EditorGUILayout.PropertyField(iterator, true, new GUILayoutOption[0]);
			GUI.enabled = true;
		}
		GUI.changed = serializedObject.ApplyModifiedProperties();

		if (GUILayout.Button("Commit") && m_font.Layout != null)
		{
			FontData fontData = SpriteFontLoader.LoadFont(m_font.Layout);
			// checking for valid pages
			bool valid = true;
			if (fontData.Common.ScaleH > 2048 || fontData.Common.ScaleW > 2048)
			{
				Debug.LogError("Width and height must not exceed 2048");
				valid = false;
			}
			if (fontData.Common.Pages != m_font.Textures.Length)
			{
				Debug.LogError("Check textures in font. Required count is " + fontData.Common.Pages, m_font);
				valid = false;
			}
			if (m_font.Textures.Any(c => c == null))
			{
				Debug.LogError("Some textures invalid (equal to null)", m_font);
				valid = false;
			}
			for (int i = 0; i < fontData.Common.Pages; i++)
			{
				Texture2D texture = m_font.Textures[i];
				if (texture.height != fontData.Common.ScaleH || texture.width != fontData.Common.ScaleW)
				{
					Debug.LogError("Size of texture '" + i + ": " + texture.name + "' not eqal to BM font texture size.", m_font);
					valid = false;
				}
			}
			if (!valid)
			{
				Debug.LogError("Font creation aborted!", m_font);
			}

			m_font.FontData = fontData;

			// check materials
			if (m_font.Materials.Length != m_font.Textures.Length)
			{
				List<Material> materials = new List<Material>(m_font.Materials);
				if (materials.Count < m_font.Textures.Length)
				{
					while (materials.Count != m_font.Textures.Length)
					{
						materials.Add(null);
					}
				}
				else
				{
					while (materials.Count != m_font.Textures.Length)
					{
						materials.RemoveAt(materials.Count - 1);
					}
				}
				m_font.Materials = materials.ToArray();
			}
			for (int i = 0; i < fontData.Common.Pages; i++)
			{
				if (m_font.Materials[i] == null || m_font.Materials[i].mainTexture != m_font.Textures[i])
				{
					m_font.Materials[i] = new Material(Shader.Find("Sprites/Default"));
					m_font.Materials[i].mainTexture = m_font.Textures[i];
					string path = AssetDatabase.GetAssetPath(m_font.Textures[i]);
					path = path.Replace(Path.GetExtension(path), ".mat");
					AssetDatabase.CreateAsset(m_font.Materials[i], path);
					AssetDatabase.Refresh();
				}
			}

			// preset geom data
			foreach (FontChar symbol in m_font.FontData.Chars)
			{
				float width = m_font.FontData.Common.ScaleW;
				float height = m_font.FontData.Common.ScaleH;
				Rect uv = new Rect();
				uv.x = symbol.X / width;
				uv.y = symbol.Y / height;
				uv.width = symbol.Width / width;
				uv.height = symbol.Height / height;
				symbol.Uv = uv;
			}
		}
		if (GUI.changed)
		{
			EditorUtility.SetDirty(m_font);
		}
	}

	private SpriteFont m_font = null;
}