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

namespace Microsoft.Xna.Framework.Input.Bridle
{
	public interface IPhysicalButton
	{
		bool Used { get; set; }
		Input Input { get; set; }
	}
}
