﻿#region License
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
using System.Collections.Generic;
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

        #region IList Extension Methods
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static T GetRandomItem<T>(this IList<T> list, IRandom randomNumberGenerator)
        {
            if (randomNumberGenerator == null) { throw new ArgumentNullException(nameof(randomNumberGenerator)); }
            if (list != null && list.Count > 0)
            {
                return list[randomNumberGenerator.Next(list.Count)];
            }
            return default(T);
        }

        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static void Shuffle<T>(this IList<T> list, IRandom randomNumberGenerator)
        {
            if (list == null) { throw new ArgumentNullException(nameof(list)); }
            if (randomNumberGenerator == null) { throw new ArgumentNullException(nameof(randomNumberGenerator)); }
            int i = list.Count - 1;
            while (i > 1)
            {
                int j = randomNumberGenerator.Next(i);
                T value = list[j];
                list[j] = list[i];
                list[i] = value;
                i--;
            }
        }
        #endregion

        #region Jagged Array Extension Methods
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y")]
        public static T[][] GetSection<T>(this T[][] byteArray, int xOffset, int yOffset, int width, int height)
        {
            if (byteArray == null) { throw new ArgumentNullException(nameof(byteArray)); }

            T[][] temp = new T[width][];

            for (int y = 0; y < temp.Length; y++)
            {
                temp[y] = new T[height];
                for (int x = 0; x < temp[y].Length; x++)
                {
                    if (yOffset + y < 0)
                    {
                        temp[y][x] = default(T);
                    }
                    else if (xOffset + x < 0)
                    {
                        temp[y][x] = default(T);
                    }
                    else if (yOffset + y >= byteArray.Length)
                    {
                        temp[y][x] = default(T);
                    }
                    else if (xOffset + x >= byteArray[yOffset + y].Length)
                    {
                        temp[y][x] = default(T);
                    }
                    else
                    {
                        temp[y][x] = byteArray[yOffset + y][xOffset + x];
                    }
                }
            }
            return temp;
        }
        #endregion
    }
}
