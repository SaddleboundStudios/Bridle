using System;

namespace Microsoft.Xna.Framework.Graphics
{
	[Flags, System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
	public enum Alignments
	{
		None = 0,
		Left = 1,
		CenterHorizontal = 2,
		Right = 4,
		Top = 8,
		CenterVertical = 16,
		Bottom = 32
	}
}
