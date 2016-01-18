//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
using System;
using UnityEngine;

/// <summary>
/// This tag gives the name of a texture file. There is one for each page in the font.
/// </summary>
[Serializable]
public class FontPage
{
	/// <summary>
	/// The page id.
	/// </summary>
	public int Id
	{
		get { return m_id; }
		set { m_id = value; }
	}

	/// <summary>
	/// The texture file name.
	/// </summary>
	public string File
	{
		get { return m_file; }
		set { m_file = value; }
	}

	[SerializeField]
	private int m_id = 0;
	[SerializeField]
	private string m_file = "";
}
