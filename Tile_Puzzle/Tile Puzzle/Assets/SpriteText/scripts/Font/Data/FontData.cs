//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class FontData
{
	public FontInfo Info
	{
		get { return m_info; }
	}

	public FontCommon Common
	{
		get { return m_common; }
	}

	public List<FontPage> Pages
	{
		get { return m_pages; }
	}

	public List<FontChar> Chars
	{
		get { return m_chars; }
	}

	public List<FontKerning> Kernings
	{
		get { return m_kernings; }
	}

	[SerializeField]
	private FontInfo m_info = new FontInfo();
	[SerializeField]
	private FontCommon m_common = new FontCommon();
	[SerializeField]
	private List<FontPage> m_pages = new List<FontPage>();
	[SerializeField]
	private List<FontChar> m_chars = new List<FontChar>();
	[SerializeField]
	private List<FontKerning> m_kernings = new List<FontKerning>();
}