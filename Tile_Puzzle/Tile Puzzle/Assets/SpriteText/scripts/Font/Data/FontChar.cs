//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This tag describes on character in the font. 
/// There is one for each included character in the font.
/// </summary>
[Serializable]
public class FontChar
{
	/// <summary>
	/// Description of the character
	/// </summary>
	public string Description
	{
		get { return m_description; }
		set { m_description = value; }
	}

	/// <summary>
	/// The character id.
	/// </summary>
	public int Id
	{
		get { return m_id; }
		set { m_id = value; }
	}

	/// <summary>
	/// The width of the character image in the texture.
	/// </summary>
	public int X
	{
		get { return m_x; }
		set { m_x = value; }
	}

	/// <summary>
	/// The height of the character image in the texture.
	/// </summary>
	public int Y
	{
		get { return m_y; }
		set { m_y = value; }
	}

	/// <summary>
	/// The width of the character image in the texture.
	/// </summary>
	public int Width
	{
		get { return m_width; }
		set { m_width = value; }
	}

	/// <summary>
	/// The height of the character image in the texture.
	/// </summary>
	public int Height
	{
		get { return m_height; }
		set { m_height = value; }
	}

	/// <summary>
	/// How much the current position should be offset when copying 
	/// the image from the texture to the screen.
	/// </summary>
	public int XOffset
	{
		get { return m_xOffset; }
		set { m_xOffset = value; }
	}

	/// <summary>
	/// How much the current position should be offset when copying 
	/// the image from the texture to the screen.
	/// </summary>
	public int YOffset
	{
		get { return m_yOffset; }
		set { m_yOffset = value; }
	}

	/// <summary>
	/// How much the current position should be advanced after drawing the character.
	/// </summary>
	public int XAdvance
	{
		get { return m_xAdvance; }
		set { m_xAdvance = value; }
	}

	/// <summary>
	/// The texture page where the character image is found.
	/// </summary>
	public int Page
	{
		get { return m_page; }
		set { m_page = value; }
	}

	/// <summary>
	/// The texture channel where the character image is found 
	/// 1 = blue, 2 = green, 4 = red, 8 = alpha, 15 = all channels.
	/// </summary>
	public int Channel
	{
		get { return m_channel; }
		set { m_channel = value; }
	}

	/// <summary>
	/// UV coordinates of the character image in the texture.
	/// </summary>
	public Rect Uv
	{
		get { return m_uv; }
		set { m_uv = value; }
	}

	public FontKerning this[SpriteFont font, char symbol]
	{
		get
		{
			if (m_font == null)
			{
				m_font = font;
			}
			if (m_font != font)
			{
				m_charhash.Clear();
			}
			FontKerning kerning = null;
			if (!m_charhash.TryGetValue(symbol, out kerning))
			{
				kerning = font.FontData.Kernings.FirstOrDefault(c => c.First == m_id && c.Second == symbol);
				m_charhash[symbol] = kerning;
			}
			return kerning;
		}
	}

	[SerializeField]
	private string m_description = "";
	[SerializeField]
	private int m_id = 0;
	[SerializeField]
	private int m_x = 0;
	[SerializeField]
	private int m_y = 0;
	[SerializeField]
	private int m_width = 0;
	[SerializeField]
	private int m_height = 0;
	[SerializeField]
	private int m_xOffset = 0;
	[SerializeField]
	private int m_yOffset = 0;
	[SerializeField]
	private int m_xAdvance = 0;
	[SerializeField]
	private int m_page = 0;
	[SerializeField]
	private int m_channel = 0;
	[SerializeField]
	private Rect m_uv = new Rect();

	private SpriteFont m_font = null;
	private Dictionary<char, FontKerning> m_charhash = new Dictionary<char, FontKerning>();
}