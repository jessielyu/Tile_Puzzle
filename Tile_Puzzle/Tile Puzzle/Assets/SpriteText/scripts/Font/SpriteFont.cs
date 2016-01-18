//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class SpriteFont : ScriptableObject
{
	public FontData FontData
	{
		get { return m_fontData; }
		set { m_fontData = value; }
	}

	public TextAsset Layout
	{
		get { return m_layout; }
	}

	public Texture2D[] Textures
	{
		get { return m_textures; }
	}

	public Material[] Materials
	{
		get { return m_materials; }
		set { m_materials = value; }
	}

	public int PixelToUnits
	{
		get { return m_pixelToUnits; }
	}

	public FontChar this[char symbol]
	{
		get
		{
			FontChar fontChar;
			if (!m_charhash.TryGetValue(symbol, out fontChar))
			{
				fontChar = m_fontData.Chars.FirstOrDefault(c => c.Id == symbol);
				m_charhash[symbol] = fontChar;
			}
			return fontChar;
		}
	}

	[SerializeField]
	private TextAsset m_layout = null;
	[SerializeField]
	private Texture2D[] m_textures = new Texture2D[0];
	[SerializeField]
	private Material[] m_materials = new Material[0];
	[SerializeField]
	private int m_pixelToUnits = 100;
	[SerializeField]
	private FontData m_fontData = new FontData();

	private Dictionary<char, FontChar> m_charhash = new Dictionary<char, FontChar>();
}