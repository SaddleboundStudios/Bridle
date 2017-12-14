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

using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Input.Bridle
{
	internal class PhysicalButton : IPhysicalButton
	{
		public Input Input { get; set; }

		public List<Control> Controls = new List<Control>();

		public int FramesDown = 0;
		
		public PhysicalButton(Input input)
		{
			Input = input.ToRaw();
		}

		public bool Used { get; set; }
	}
}
