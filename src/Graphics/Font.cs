using System.Collections.Generic;
using System.IO;

namespace Microsoft.Xna.Framework.Graphics
{
	public class Font
	{
		private readonly SpriteBatch _spriteBatch;

		private readonly Texture2D[] _texMonospace;
		private readonly Texture2D[] _texProportional;
		internal readonly byte[] FontWidths;

		public Font(Renderer graphics, string proportionalFont, Color[] proportionalFontColors, string monospaceFont, Color[] monospaceFontColors, string fontWidths)
		{
			_spriteBatch = graphics.SpriteBatch;
			_texProportional = graphics.LoadTextureColors(proportionalFont, proportionalFontColors);
			_texMonospace = graphics.LoadTextureColors(monospaceFont, monospaceFontColors);
			FontWidths = File.ReadAllBytes(fontWidths);
		}

		public void RenderFormattedString(int x, int y, FontSize size, Color[] colors, List<TextCharData> text)
		{
			int x2 = 0;
			foreach (TextCharData c in text)
			{
				if (c.Monospace)
				{
					for (int i = colors.Length - 1; i >= 0; i--)
					{
						_spriteBatch.Draw(_texMonospace[i],
						new Rectangle(x + x2, y, (int)size * 16, (int)size * 16),
						new Rectangle((c.Character & 0xF) * 16, (c.Character >> 4) * 16, 16, 16),
						colors[i]);
					}
				}
				else
				{
					for (int i = colors.Length-1; i >= 0; i--)
					{
						_spriteBatch.Draw(_texProportional[i],
							new Rectangle(x + x2, y, (int)size * 16, (int)size * 16),
							new Rectangle((c.Character & 0xF) * 16, (c.Character >> 4) * 16, 16, 16),
							colors[i]);
					}
				}

				x2 += (int)size * c.GetWidth(this);
			}
		}

		public static List<TextCharData> FormatString(string s)
		{
			List<TextCharData> text = new List<TextCharData>();

			bool nextIsControl = false, monospace = false, inverted = false, tempMonospace = false;

			int nextIsColor = 0;

			foreach (char c in s)
			{
				if (nextIsColor > 0)
				{
					nextIsColor--;
				}
				else if (!nextIsControl || c == '\\' || c == '^' || c == 'v' || c == 'V' || c == '<' || c == '>' || c == '*')
				{
					if (!nextIsControl && c == '\\')
					{
						nextIsControl = true;
					}
					else
					{
						char d = c;
						if (nextIsControl)
						{
							switch (c)
							{
								case '^':
									d = '\x1E';
									break;
								case 'V':
								case 'v':
									d = '\x1F';
									break;
								case '<':
									d = '\x11';
									break;
								case '>':
									d = '\x10';
									break;
								case '*':
									d = '\x09';
									break;
							}
						}

						text.Add(new TextCharData(d, tempMonospace ? !monospace : monospace, false/*, inverted*/));
						tempMonospace = false;
						nextIsControl = false;
					}
				}
				else
				{
					switch (c)
					{
						case 'C':
						case 'c':
							nextIsColor = 3;
							break;
						case 'M':
							monospace = !monospace;
							break;
						case 'm':
							tempMonospace = true;
							break;
						case 'N':
						case 'n':
							text.Add(new TextCharData(' ', false, true/*, false*/));
							break;
						case 'I':
							inverted = !inverted;
							break;
					}

					nextIsControl = false;
				}
			}
			return text;
		}

		public void DrawCharacter(int x, int y, int c, FontSize size, Color[] colors)
		{
			for (int i = _texMonospace.Length - 1; i >= 0; i--)
			{
				_spriteBatch.Draw(_texMonospace[i], new Rectangle(x, y, (int) size*16, (int) size*16),
					new Rectangle((c & 0xF)*16, (c >> 4)*16, 16, 16), colors[i]);
			}
		}

		public void DrawString(int x, int y, FontSize size, Color[] colors, string text)
		{
			if (text == null)
			{
				return;
			}

			RenderFormattedString(x, y, size, colors, FormatString(text));
		}

		public void DrawString(Rectangle rectangle, FontSize size, Color[] colors, string text, Alignments alignments = Alignments.Left | Alignments.CenterVertical)
		{
			if (text == null)
			{
				return;
			}

			List<TextCharData> s = FormatString(text);
			int w2 = 0;
			if (rectangle.Width > 0 && (alignments.HasFlag(Alignments.Right) || alignments.HasFlag(Alignments.CenterHorizontal)))
			{
				for (int i = 0; i < s.Count; i++)
				{
					if (s[i].Monospace || i != s.Count - 1)
					{
						w2 += (int)size * s[i].GetWidth(this);
					}
					else
					{
						w2 += (int)size * (s[i].GetWidth(this) - 1);
					}
				}
			}

			int x = rectangle.X, y = rectangle.Y;

			if (rectangle.Width > 0)
			{
				if (alignments.HasFlag(Alignments.Right))
				{
					x += rectangle.Width - w2;
				}
				else if (alignments.HasFlag(Alignments.CenterHorizontal))
				{
					x += (rectangle.Width - w2) / 2;
				}
			}

			if (rectangle.Height > 0)
			{
				if (alignments.HasFlag(Alignments.Bottom))
				{
					y += rectangle.Height - ((int)size * 16);
				}
				else if (alignments.HasFlag(Alignments.CenterVertical))
				{
					y += (rectangle.Height - ((int)size * 16)) / 2;
				}
			}

			RenderFormattedString(x, y, size, colors, s);
		}

		public void DrawLimitedString(int x, int y, int w, int h, FontSize size, Color[] colors, string s)
		{
			List<TextCharData> sentence = FormatString(s);
			List<TextWordData> words = new List<TextWordData>();
			List<TextCharData> word = new List<TextCharData>();

			for (int i = 0; i <= sentence.Count; i++)
			{
				TextCharData c = i < sentence.Count ? sentence[i] : TextCharData.Space();
				if (i == sentence.Count || c.Character == ' ' || c.Newline)
				{
					if (word.Count > 0)
					{
						words.Add(new TextWordData(word, c));
						word.Clear();
					}

					if (c.Newline)
					{
						words.Add(new TextWordData(true));
					}
				}
				else
				{
					word.Add(c);
				}
			}

			bool nextNewline = false;
			int x2 = 0;
			int y2 = 0;
			for (int i = 0; i < words.Count; i++)
			{
				if (words[i].Newline)
				{
					y2 += (int)size * 16;
					x2 = 0;
				}
				else
				{
					if (x2 + words[i].GetWidth(this) > w || nextNewline)
					{
						y2 += (int)size * 16;
						x2 = 0;
						nextNewline = false;
					}

					if (x2 + (int)size * (words[i].GetWidth(this) + words[i + 1].GetWidth(this)) > w)
					{
						// Don't draw space.
						nextNewline = true;
					}
					else if (i == words.Count - 1 || words[i + 1].Newline)
					{
						// Don't draw space.
					}
					else
					{
						// Draw space.
						words[i].Chars.Add(words[i].Space);
					}

					RenderFormattedString(x + x2, y + y2, size, colors, words[i].Chars);

					x2 += (int)size * words[i].GetWidth(this);
				}
			}
		}
	}
}
