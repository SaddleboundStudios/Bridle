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
using System.Linq;

namespace Microsoft.Xna.Framework.Utilities
{
    internal static class ExtensionMethods
    {
        #region Type Extension Methods
        public static bool IsSameOrSubclassOf(this Type potentialDescendant, Type potentialBase)
        {
            if (potentialDescendant == null) { throw new ArgumentNullException(nameof(potentialDescendant)); }
            return potentialDescendant.IsSubclassOf(potentialBase) || potentialDescendant == potentialBase;
        }

        public static bool ImplementsInterface(this Type potentialImplementer, Type interfaceType)
        {
            if (potentialImplementer == null) { throw new ArgumentNullException(nameof(potentialImplementer)); }
            return potentialImplementer.GetInterfaces().Contains(interfaceType);
        }
        #endregion
    }
}
