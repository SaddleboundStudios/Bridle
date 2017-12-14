namespace Microsoft.Xna.Framework.Graphics
{
	public class TextCharData
	{
		public char Character;
		public bool Monospace, Newline/*, Inverted*/;

		public TextCharData(char character, bool monospace, bool newline)
		{
			Character = character;
			Monospace = monospace;
			Newline = newline;
		}

		public int GetWidth(Font font)
		{
			if (Monospace)
			{
				return 16;
			}
			if (Character == 0x20 || font.FontWidths[Character] == 16)
			{
				return font.FontWidths[Character];
			}

			return font.FontWidths[Character] + 1;
		}

		public static TextCharData Space()
		{
			return new TextCharData(' ', false, true/*, false*/);
		}
	}
}
