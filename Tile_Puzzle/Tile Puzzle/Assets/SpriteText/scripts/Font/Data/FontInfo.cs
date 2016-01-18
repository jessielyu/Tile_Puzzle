//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
using System;
using UnityEngine;

/// <summary>
/// This tag holds information on how the font was generated.
/// </summary>
[Serializable]
public class FontInfo
{
	/// <summary>
	/// This is the name of the true type font.
	/// </summary>
	public string Face
	{
		get { return m_face; }
		set { m_face = value; }
	}

	/// <summary>
	/// The size of the true type font.
	/// </summary>
	public int Size
	{
		get { return m_size; }
		set { m_size = value; }
	}

	/// <summary>
	/// The font is bold.
	/// </summary>
	public bool Bold
	{
		get { return m_bold; }
		set { m_bold = value; }
	}

	/// <summary>
	/// The font is italic.
	/// </summary>
	public bool Italic
	{
		get { return m_italic; }
		set { m_italic = value; }
	}

	/// <summary>
	/// The name of the OEM charset used (when not unicode).
	/// </summary>
	public string Charset
	{
		get { return m_charset; }
		set { m_charset = value; }
	}

	/// <summary>
	/// Set to true if it is the unicode charset.
	/// </summary>
	public bool Unicode
	{
		get { return m_unicode; }
		set { m_unicode = value; }
	}

	/// <summary>
	/// The font height stretch in percentage. 100% means no stretch.
	/// </summary>
	public int StretchHeight
	{
		get { return m_stretchHeight; }
		set { m_stretchHeight = value; }
	}

	/// <summary>
	/// Set to 1 if smoothing was turned on.
	/// </summary>
	public bool Smooth
	{
		get { return m_smooth; }
		set { m_smooth = value; }
	}

	/// <summary>
	/// The supersampling level used. 1 means no supersampling was used.
	/// </summary>
	public int SuperSampling
	{
		get { return m_superSampling; }
		set { m_superSampling = value; }
	}

	/// <summary>
	/// The padding for each character (up, right, down, left).
	/// </summary>
	public Rect Padding
	{
		get { return m_padding; }
		set { m_padding = value; }
	}

	/// <summary>
	/// The spacing for each character (horizontal, vertical).
	/// </summary>
	public Vector2 Spacing
	{
		get { return m_spacing; }
		set { m_spacing = value; }
	}

	/// <summary>
	/// The outline thickness for the characters.
	/// </summary>
	public int Outline
	{
		get { return m_outline; }
		set { m_outline = value; }
	}

	[SerializeField]
	private string m_face = "";
	[SerializeField]
	private int m_size = 0;
	[SerializeField]
	private bool m_bold = false;
	[SerializeField]
	private bool m_italic = false;
	[SerializeField]
	private string m_charset = "";
	[SerializeField]
	private bool m_unicode = true;
	[SerializeField]
	private int m_stretchHeight = 0;
	[SerializeField]
	private bool m_smooth = false;
	[SerializeField]
	private int m_superSampling = 0;
	[SerializeField]
	private Rect m_padding = new Rect();
	[SerializeField]
	private Vector2 m_spacing = new Vector2();
	[SerializeField]
	private int m_outline = 0;
}
