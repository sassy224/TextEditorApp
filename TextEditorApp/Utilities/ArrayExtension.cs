using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorApp.Utilities
{
    public static class ArrayExtension
    {
        /// <summary>
        /// Splits an array into several smaller arrays.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array to split.</param>
        /// <param name="size">The size of the smaller arrays.</param>
        /// <returns>An array containing smaller arrays.</returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, int size)
        {
            //If size to be splitted > length of array, return this array as the only member of the splitted array
            if (size > array.Length)
                yield return array;

            for (var i = 0; i < (float)array.Length / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }

        /// <summary>
        /// Splits a string into smaller substrings
        /// </summary>
        /// <param name="str">The string to be splitted</param>
        /// <param name="maxLength">The maximum size of the substring</param>
        /// <returns>Array of sub strings</returns>
        public static IEnumerable<string> SplitByLength(this string str, int maxLength)
        {
            for (int index = 0; index < str.Length; index += maxLength)
            {
                yield return str.Substring(index, Math.Min(maxLength, str.Length - index));
            }
        }
    }
}