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

namespace Microsoft.Xna.Framework.Input
{
	[Flags]
	public enum MouseButtons
	{
		None = 0,
		Mouse1 = 1,
		Mouse2 = 2,
		Mouse3 = 4,
		Mouse4 = 8,
		Mouse5 = 16,
		Mouse6 = 32,
		Mouse7 = 64,
		Mouse8 = 128,
		Mouse9 = 256,
		Mouse10 = 512,
		Mouse11 = 1024,
		Mouse12 = 2048,
		Mouse13 = 4096,
		Mouse14 = 8192,
		Mouse15 = 16384,
		Mouse16 = 32768,
		ScrollUp = 65536,
		ScrollDown = 131072,
		TiltLeft = 262144,
		TiltRight = 524288,
	}
}
