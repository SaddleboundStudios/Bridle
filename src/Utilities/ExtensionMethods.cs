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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

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

        #region String Extension Methods
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "CA1063 does not apply to argument \"text\", as it requires it to be non-null as an extension method of the class.")]
        public static string ReplaceFirst(this string @string, string search, string replace)
        {
            if (search != null && replace != null)
            {
                int pos = @string.IndexOf(search, StringComparison.Ordinal);
                if (pos < 0)
                {
                    return @string;
                }
                return @string.Substring(0, pos) + replace + @string.Substring(pos + search.Length);
            }
            return @string;
        }

        #region FNV-1a non-cryptographic hash functions.
        // Retrieved from http://gist.github.com/rasmuskl/3786618
        // Adapted from: http://github.com/jakedouglas/fnv-java
        // Licensed under the MIT License as of 6/13/2014 at 7:54AM

        const uint Fnv32Offset = 0x811C9DC5;
        const uint Fnv32Prime = 0x1000193;
        const ulong Fnv64Offset = 0xCBF29CE484222325;
        const ulong Fnv64Prime = 0x100000001B3;

        /// <summary>
        /// FNV-1a (64-bit) non-cryptographic hash function.
        /// </summary>
        /// <param name="string">The string to hash.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Fnv")]
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public static ulong ToFnv64Hash(this string @string)
        {
            ulong hash = Fnv64Offset;
            byte[] bytes = Encoding.UTF8.GetBytes(@string);
            for (int i = 0; i < bytes.Length; i++)
            {
                hash = hash ^ bytes[i];
                hash *= Fnv64Prime;
            }
            return hash;
        }

        /// <summary>
        /// FNV-1a (32-bit) non-cryptographic hash function.
        /// </summary>
        /// <param name="string">The string to hash.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Fnv")]
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public static uint ToFnv32Hash(this string @string)
        {
            uint hash = Fnv32Offset;
            byte[] bytes = Encoding.UTF8.GetBytes(@string);
            for (int i = 0; i < bytes.Length; i++)
            {
                hash = hash ^ bytes[i];
                hash *= Fnv32Prime;
            }
            return hash;
        }

        /// <summary>
        /// FNV-1a (24-bit) non-cryptographic hash function.
        /// </summary>
        /// <param name="string">The string to hash.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Fnv")]
        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public static uint ToFnv24Hash(this string @string)
        {
            uint hash = Fnv32Offset;
            byte[] bytes = Encoding.UTF8.GetBytes(@string);
            for (int i = 0; i < bytes.Length; i++)
            {
                hash = hash ^ bytes[i];
                hash *= Fnv32Prime;
            }

            hash = (hash >> 24) ^ (hash & 0xFFFFFF);

            return hash;
        }
        #endregion
        #endregion
    }
}
