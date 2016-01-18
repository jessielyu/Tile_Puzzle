//-----------------------------------------------------------------------------------------------
// author: Ivan Murashka 
// e-mail: iclickable@gmail.com
//-----------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

internal static class SpriteTextParser
{
	public static List<string> ParseText(SpriteText spriteText, string text)
	{
		List<string> lines = new List<string>();

		SpriteFont font = spriteText.Font;
		float maxLineWidth = spriteText.LineWidth * font.PixelToUnits;
		float spaceWidth = GetWidth(font, ' ');
		int index = 0;
		bool splittedStart = false;

		SetBuffer(text.Length);
		int debugIter = 0;
		do
		{
			int wordsCount = 0;
			float lineWidth = 0.0f;
			string line = "";

			while (index < text.Length)
			{
				int storeIndex = index;
				string word = GetWord(text, ref index);
				float width = GetWidth(font, word);
				if (lineWidth + width > maxLineWidth)
				{
					if (wordsCount == 0)
					{
						splittedStart = true;
						line = SubWord(font, word, maxLineWidth);
						index -= word.Length - line.Length;
					}
					else if (wordsCount == 1 && splittedStart)
					{
						word = line + word;
						line = SubWord(font, word, maxLineWidth);
						index -= word.Length - line.Length;
					}
					else
					{
						splittedStart = false;
						index = storeIndex;
					}
					break;
				}
				wordsCount++;
				lineWidth += width + spaceWidth;
				line += word;
			}
			lines.Add(line);
		}
		while (index < text.Length && debugIter++ < 40);

		return lines;
	}

	private static string SubWord(SpriteFont font, string word, float maxLineWidth)
	{
		float lineWidth = 0.0f;
		for (int i = 0; i < word.Length; i++)
		{
			lineWidth += GetWidth(font, word[i]);
			if (lineWidth > maxLineWidth)
			{
				i = Math.Max(1, i);
				return word.Substring(0, i);
			}
		}
		return word;
	}

	private static string GetWord(string text, ref int index)
	{
		int length = 0;
		do
		{
			char c = text[index];
			s_charBuffer[length] = c;
			length++;
		}
		while (text[index++] != ' ' && index < text.Length);
		return new string(s_charBuffer, 0, length);
	}

	private static float GetWidth(SpriteFont font, string word)
	{
		float width = 0.0f;
		int length = word[word.Length - 1] == ' ' ? word.Length - 1 : word.Length;
		for (int index = 0; index < length; index++)
		{
			width += GetWidth(font, word[index]);
		}
		return width;
	}

	private static float GetWidth(SpriteFont font, char symbol)
	{
		return GetWidth(font, font[symbol]);
	}

	private static float GetWidth(SpriteFont font, FontChar fontChar)
	{
		return fontChar.XAdvance + font.FontData.Info.Spacing.x;
	}

	private static void SetBuffer(int length)
	{
		if (s_charBuffer.Length < length)
		{
			s_charBuffer = new char[length];
		}
	}

	private static char[] s_charBuffer = new char[16];
}