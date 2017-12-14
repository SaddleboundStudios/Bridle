#region License
/* Bridle
 * Copyright 2017 Walter Barrett
 *
 * This project is released under the Microsoft Public
 * License. This file is dual-licensed under the MS-PL
 * and the 3-Clause BSD License.
 *
 * See LICENSE.MD for details.
 */
#endregion

using System;

namespace Microsoft.Xna.Framework.Input.Bridle
{
	[Flags]
	public enum ModifierKeys
	{
		None = 0,
		Alt = 1,
		Ctrl = 2,
		Shift = 4
	}
}
