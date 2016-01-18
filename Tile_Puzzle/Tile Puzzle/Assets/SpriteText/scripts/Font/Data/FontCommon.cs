//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
using System;
using UnityEngine;

/// <summary>
/// This tag holds information common to all characters.
/// </summary>
[Serializable]
public class FontCommon
{
	/// <summary>
	/// This is the distance in pixels between each line of text.
	/// </summary>
	public int LineHeight
	{
		get { return m_lineHeight; }
		set { m_lineHeight = value; }
	}

	/// <summary>
	/// The number of pixels from the absolute top of the line to the base of the characters.
	/// </summary>
	public int Base
	{
		get { return m_base; }
		set { m_base = value; }
	}

	/// <summary>
	/// The width of the texture, normally used to scale the x pos of the character image.
	/// </summary>
	public int ScaleW
	{
		get { return m_scaleW; }
		set { m_scaleW = value; }
	}

	/// <summary>
	/// The height of the texture, normally used to scale the y pos of the character image.
	/// </summary>
	public int ScaleH
	{
		get { return m_scaleH; }
		set { m_scaleH = value; }
	}

	/// <summary>
	/// The number of texture pages included in the font.
	/// </summary>
	public int Pages
	{
		get { return m_pages; }
		set { m_pages = value; }
	}

	/// <summary>
	/// Set to true if the monochrome characters have been packed into each of the texture channels.
	/// In this case alphaChnl describes what is stored in each channel.
	/// </summary>
	public bool Packed
	{
		get { return m_packed; }
		set { m_packed = value; }
	}

	/// <summary>
	/// 0 if the channel holds the glyph data, 
	/// 1 if it holds the outline, 
	/// 2 if it holds the glyph and the outline, 
	/// 3 if its set to zero, and 
	/// 4 if its set to one.
	/// </summary>
	public int AlphaChannel
	{
		get { return m_alphaChannel; }
		set { m_alphaChannel = value; }
	}


	/// <summary>
	/// 0 if the channel holds the glyph data, 
	/// 1 if it holds the outline, 
	/// 2 if it holds the glyph and the outline, 
	/// 3 if its set to zero, and 
	/// 4 if its set to one.
	/// </summary>
	public int RedChannel
	{
		get { return m_redChannel; }
		set { m_redChannel = value; }
	}


	/// <summary>
	/// 0 if the channel holds the glyph data, 
	/// 1 if it holds the outline, 
	/// 2 if it holds the glyph and the outline, 
	/// 3 if its set to zero, and 
	/// 4 if its set to one.
	/// </summary>
	public int GreenChannel
	{
		get { return m_greenChannel; }
		set { m_greenChannel = value; }
	}


	/// <summary>
	/// 0 if the channel holds the glyph data, 
	/// 1 if it holds the outline, 
	/// 2 if it holds the glyph and the outline, 
	/// 3 if its set to zero, and 
	/// 4 if its set to one.
	/// </summary>
	public int BlueChannel
	{
		get { return m_blueChannel; }
		set { m_blueChannel = value; }
	}

	[SerializeField]
	private int m_lineHeight = 0;
	[SerializeField]
	private int m_base = 0;
	[SerializeField]
	private int m_scaleW = 0;
	[SerializeField]
	private int m_scaleH = 0;
	[SerializeField]
	private int m_pages = 0;
	[SerializeField]
	private bool m_packed = false;
	[SerializeField]
	private int m_alphaChannel = 0;
	[SerializeField]
	private int m_redChannel = 0;
	[SerializeField]
	private int m_greenChannel = 0;
	[SerializeField]
	private int m_blueChannel = 0;
}
