using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public static class SpriteFontLoader
{
	public static FontData LoadFont(TextAsset fontLayout)
	{
		try
		{
			return LoadFontFromXmlFile(fontLayout);
		}
		catch (XmlException)
		{
			return LoadFontFromTextFile(fontLayout);
		}
	}

	public static FontData LoadFontFromTextFile(TextAsset fontLayout)
	{
		FontData font = new FontData();
		string[] lines = fontLayout.text.Split('\n');
		font.Pages.Clear();
		font.Chars.Clear();
		font.Kernings.Clear();
		foreach (string line in lines)
		{
			Dictionary<string, string> table = ParseLine(line);
			if (table.Count == 0)
			{
				continue;
			}
			switch (table["section"])
			{
				case "info":
					font.Info.Face = ParseString(table, "face");
					font.Info.Size = ParseInt(table, "size");
					font.Info.Bold = ParseBool(table, "bold");
					font.Info.Italic = ParseBool(table, "italic");
					font.Info.Charset = ParseString(table, "charset");
					font.Info.Unicode = ParseBool(table, "unicode");
					font.Info.StretchHeight = ParseInt(table, "stretchH");
					font.Info.Smooth = ParseBool(table, "smooth");
					font.Info.SuperSampling = ParseInt(table, "aa");
					font.Info.Padding = ParseRect(table, "padding");
					font.Info.Spacing = ParseVector2(table, "spacing");
					font.Info.Outline = ParseInt(table, "outline");
					break;
				case "common":
					font.Common.LineHeight = ParseInt(table, "lineHeight");
					font.Common.Base = ParseInt(table, "base");
					font.Common.ScaleW = ParseInt(table, "scaleW");
					font.Common.ScaleH = ParseInt(table, "scaleW");
					font.Common.Pages = ParseInt(table, "pages");
					font.Common.Packed = ParseBool(table, "packed");
					font.Common.AlphaChannel = ParseInt(table, "alphaChnl");
					font.Common.RedChannel = ParseInt(table, "redChnl");
					font.Common.GreenChannel = ParseInt(table, "greenChnl");
					font.Common.BlueChannel = ParseInt(table, "blueChnl");
					break;
				case "page":
					FontPage page = new FontPage();
					page.Id = ParseInt(table, "id");
					page.File = ParseString(table, "file");
					font.Pages.Add(page);
					break;
				case "char":
					FontChar ch = new FontChar();
					ch.Id = ParseInt(table, "id");
					ch.X = ParseInt(table, "x");
					ch.Y = ParseInt(table, "y");
					ch.Width = ParseInt(table, "width");
					ch.Height = ParseInt(table, "height");
					ch.XOffset = ParseInt(table, "xoffset");
					ch.YOffset = ParseInt(table, "yoffset");
					ch.XAdvance = ParseInt(table, "xadvance");
					ch.Page = ParseInt(table, "page");
					ch.Channel = ParseInt(table, "chnl");
					ch.Description = string.Format("Char: [{0}]; Code: [{1}]", (char)ch.Id, ch.Id);
					font.Chars.Add(ch);
					break;
				case "kerning":
					FontKerning key = new FontKerning();
					key.First = ParseInt(table, "first");
					key.Second = ParseInt(table, "second");
					key.Amount = ParseInt(table, "amount");
					key.Description = string.Format("[{0}] [{1}] Amout: [{2}]", (char)key.First, (char)key.Second, key.Amount);
					font.Kernings.Add(key);
					break;
			}
		}
		return font;
	}

	public static FontData LoadFontFromXmlFile(TextAsset fontLayout)
	{
		FontData font = new FontData();
		XmlDocument document = new XmlDocument();
		document.LoadXml(fontLayout.text);
		XmlNode root = document.DocumentElement;

		// load the basic attributes
		XmlNode secton = root.SelectSingleNode("info");
		font.Info.Face = ParseString(secton, "face");
		font.Info.Size = ParseInt(secton, "size");
		font.Info.Bold = ParseBool(secton, "bold");
		font.Info.Italic = ParseBool(secton, "italic");
		font.Info.Unicode = ParseBool(secton, "unicode");
		font.Info.StretchHeight = ParseInt(secton, "stretchH");
		font.Info.Charset = ParseString(secton, "charset");
		font.Info.Smooth = ParseBool(secton, "smooth");
		font.Info.SuperSampling = ParseInt(secton, "aa");
		font.Info.Padding = ParseRect(secton, "padding");
		font.Info.Spacing = ParseVector2(secton, "spacing");
		font.Info.Outline = ParseInt(secton, "outline");

		// common attributes
		secton = root.SelectSingleNode("common");
		font.Common.LineHeight = ParseInt(secton, "lineHeight");
		font.Common.Base = ParseInt(secton, "base");
		font.Common.ScaleW = ParseInt(secton, "scaleW");
		font.Common.ScaleH = ParseInt(secton, "scaleH");
		font.Common.Pages = ParseInt(secton, "pages");
		font.Common.Packed = ParseBool(secton, "packed");
		font.Common.AlphaChannel = ParseInt(secton, "alphaChnl");
		font.Common.RedChannel = ParseInt(secton, "redChnl");
		font.Common.GreenChannel = ParseInt(secton, "greenChnl");
		font.Common.BlueChannel = ParseInt(secton, "blueChnl");

		// load texture information
		font.Pages.Clear();
		foreach (XmlNode node in root.SelectNodes("pages/page"))
		{
			FontPage page = new FontPage();
			page.Id = ParseInt(node, "id");
			page.File = ParseString(node, "file");
			font.Pages.Add(page);
		}

		// load character information
		font.Chars.Clear();
		foreach (XmlNode node in root.SelectNodes("chars/char"))
		{
			FontChar ch = new FontChar();
			ch.Id = ParseInt(node, "id");
			ch.X = ParseInt(node, "x");
			ch.Y = ParseInt(node, "y");
			ch.Width = ParseInt(node, "width");
			ch.Height = ParseInt(node, "height");
			ch.XOffset = ParseInt(node, "xoffset");
			ch.YOffset = ParseInt(node, "yoffset");
			ch.XAdvance = ParseInt(node, "xadvance");
			ch.Page = ParseInt(node, "page");
			ch.Channel = ParseInt(node, "chnl");
			ch.Description = string.Format("Char: [{0}]; Code: [{1}]", (char)ch.Id, ch.Id);
			font.Chars.Add(ch);
		}

		// loading kerning information
		font.Kernings.Clear();
		foreach (XmlNode node in root.SelectNodes("kernings/kerning"))
		{
			FontKerning key = new FontKerning();
			key.First = ParseInt(node, "first");
			key.Second = ParseInt(node, "second");
			key.Amount = ParseInt(node, "amount");
			key.Description = string.Format("[{0}] [{1}] Amout: [{2}]", (char)key.First, (char)key.Second, key.Amount);
			font.Kernings.Add(key);
		}
		return font;
	}

	private static Rect ParseRect(XmlNode section, string key)
	{
		string valueString = ParseString(section, key, "0,0,0,0");
		return ParseRect(key, valueString, section.Name);
	}

	private static Rect ParseRect(Dictionary<string, string> table, string key)
	{
		string valueString = ParseString(table, key, "0,0,0,0");
		return ParseRect(key, valueString, table["section"]);
	}

	private static Rect ParseRect(string key, string valueString, string section)
	{
		Rect value = new Rect(0, 0, 0, 0);
		String[] padding = valueString.Split(',');
		if (padding.Length != 4)
		{
			Debug.LogWarning(string.Format("Can't parse value '{0}' to Rect. Set default value '{1}'.\nkey={2}; section={3}.", valueString, value, key, section));
			return value;
		}
		int left, top, width, height;
		if (!int.TryParse(padding[0], out left))
		{
			Debug.LogWarning(string.Format("Can't parse value '{0}' to Rect. Set default value '{1}'.\nkey={2}; section={3}.", valueString, value, key, section));
			return value;
		}
		if (!int.TryParse(padding[1], out top))
		{
			Debug.LogWarning(string.Format("Can't parse value '{0}' to Rect. Set default value '{1}'.\nkey={2}; section={3}.", valueString, value, key, section));
			return value;
		}
		if (!int.TryParse(padding[2], out width))
		{
			Debug.LogWarning(string.Format("Can't parse value '{0}' to Rect. Set default value '{1}'.\nkey={2}; section={3}.", valueString, value, key, section));
			return value;
		}
		if (!int.TryParse(padding[3], out height))
		{
			Debug.LogWarning(string.Format("Can't parse value '{0}' to Rect. Set default value '{1}'.\nkey={2}; section={3}.", valueString, value, key, section));
			return value;
		}
		return new Rect(left, top, width, height);
	}

	private static Vector2 ParseVector2(XmlNode section, string key)
	{
		string valueString = ParseString(section, key, "0,0");
		return ParseVector2(key, valueString, section.Name);
	}

	private static Vector2 ParseVector2(Dictionary<string, string> table, string key)
	{
		string valueString = ParseString(table, key, "0,0");
		return ParseVector2(key, valueString, table["section"]);
	}

	private static Vector2 ParseVector2(string key, string valueString, string section)
	{
		Vector2 value = new Vector2(0, 0);
		String[] padding = valueString.Split(',');
		if (padding.Length != 2)
		{
			Debug.LogWarning(string.Format("Can't parse value '{0}' to Rect. Set default value '{1}'.\nkey={2}; section={3}.", valueString, value, key, section));
			return value;
		}
		int x, y;
		if (!int.TryParse(padding[0], out x))
		{
			Debug.LogWarning(string.Format("Can't parse value '{0}' to Rect. Set default value '{1}'.\nkey={2}; section={3}.", valueString, value, key, section));
			return value;
		}
		if (!int.TryParse(padding[1], out y))
		{
			Debug.LogWarning(string.Format("Can't parse value '{0}' to Rect. Set default value '{1}'.\nkey={2}; section={3}.", valueString, value, key, section));
			return value;
		}
		return new Vector2(x, y);
	}

	private static bool ParseBool(XmlNode section, string key)
	{
		return ParseInt(section, key) != 0;
	}

	private static bool ParseBool(Dictionary<string, string> table, string key)
	{
		return ParseInt(table, key) != 0;
	}

	private static int ParseInt(XmlNode section, string key)
	{
		string valueString = ParseString(section, key, "0");
		return ParseInt(key, valueString, section.Name);
	}

	private static int ParseInt(Dictionary<string, string> table, string key, int defaultValue = 0)
	{
		string valueString = ParseString(table, key, defaultValue.ToString());
		return ParseInt(key, valueString, table["section"]);
	}

	private static int ParseInt(string key, string valueString, string section)
	{
		int value;
		if (!int.TryParse(valueString, out value))
		{
			Debug.LogWarning(string.Format("Can't parse value '{0}' to int. Set default value '{1}'.\nkey={2}; section={3}.", valueString, 0, key, section));
			value = 0;
		}
		return value;
	}

	private static string ParseString(XmlNode node, string key, string defaultValue = "")
	{
		XmlAttribute attribute = node.Attributes[key];
		string value;
		if (attribute == null)
		{
			Debug.LogWarning(string.Format("Key '{0}' not found in section '{1}'. Set default value '{2}'", key, node.Name, defaultValue));
			value = defaultValue;
		}
		else
		{
			value = attribute.Value;
		}
		return value;
	}

	private static string ParseString(Dictionary<string, string> table, string key, string defaultValue = "")
	{
		string value;
		if (!table.TryGetValue(key, out value))
		{
			Debug.LogWarning(string.Format("Key '{0}' not found in section '{1}'. Set default value '{2}'", key, table["section"], defaultValue));
			value = defaultValue;
		}
		return value;
	}

	private static Dictionary<string, string> ParseLine(string line)
	{
		Dictionary<string, string> result = new Dictionary<string, string>();
		string[] parts = line.Split(new[] { ' ', '\n', '\r', '=' }, StringSplitOptions.RemoveEmptyEntries);
		if (parts.Length != 0)
		{
			result["section"] = parts[0];
			for (int i = 1; i + 1 < parts.Length; i += 2)
			{
				if (parts[i + 1].Length > 1 && parts[i + 1].StartsWith("\"") && parts[i + 1].EndsWith("\""))
				{
					parts[i + 1] = parts[i + 1].Substring(1, parts[i + 1].Length - 2);
				}
				result[parts[i]] = parts[i + 1];
			}
		}
		return result;
	}
}