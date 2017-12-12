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

namespace Microsoft.Xna.Framework.Utilities
{
    public interface IRandom
    {
        int Next();
        int Next(int maxValue);
        //int Next(int minValue, int maxValue);

        uint NextUInt32();
        uint NextUInt32(uint maxValue);
        //uint NextUInt32(uint minValue, uint maxValue);

        double NextDouble();

        byte[] NextBytes(byte[] buffer);
    }
}
