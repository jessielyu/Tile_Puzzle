//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
using System;
using UnityEngine;

/// <summary>
/// The kerning information is used to adjust the distance between certain characters, 
/// e.g. some characters should be placed closer to each other than others.
/// </summary>
[Serializable]
public class FontKerning
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
	/// The first character id.
	/// </summary>
	public int First
	{
		get { return m_first; }
		set { m_first = value; }
	}

	/// <summary>
	/// The second character id.
	/// </summary>
	public int Second
	{
		get { return m_second; }
		set { m_second = value; }
	}

	/// <summary>
	/// How much the x position should be adjusted when drawing 
	/// the second character immediately following the first
	/// </summary>
	public int Amount
	{
		get { return m_amount; }
		set { m_amount = value; }
	}

	[SerializeField]
	private string m_description = "";
	[SerializeField]
	private int m_first = 0;
	[SerializeField]
	private int m_second = 0;
	[SerializeField]
	private int m_amount = 0;
}