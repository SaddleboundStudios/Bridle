using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
	public struct TextWordData
	{
		public List<TextCharData> Chars;
		public bool Newline;
		public TextCharData Space;

		public int GetWidth(Font font)
		{
			if (Newline)
			{
				return 0;
			}

			int w = 0;
			foreach (TextCharData c in Chars)
			{
				w += c.GetWidth(font);
			}

			return w;
		}

		public TextWordData(List<TextCharData> chars, TextCharData space)
		{
			Chars = new List<TextCharData>(chars);
			Newline = false;
			Space = space;
		}

		public TextWordData(bool newline)
		{
			Chars = null;
			Newline = newline;
			Space = null;
		}
	}
}
