using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditorApp.Utilities
{
    public class ListUtils
    {
        /// <summary>
        /// Get all known color names of the system
        /// </summary>
        /// <returns>Array of color names</returns>
        public static string[] GetKnownColorNames()
        {
            return System.Enum.GetNames(typeof(KnownColor));
        }

        /// <summary>
        /// Get all installed font family names
        /// </summary>
        /// <returns>Array of FontFamily object</returns>
        public static FontFamily[] GetInstalledFontFamilies()
        {
            return new InstalledFontCollection().Families;
        }

        /// <summary>
        /// Get a predefined list of font sizes
        /// </summary>
        /// <returns>List of sizes as int</returns>
        public static List<int> GetFontSizes()
        {
            List<int> sizes = new List<int>();
            for (int i = 5; i < 60; i += 5)
            {
                sizes.Add(i);
            }
            return sizes;
        }
    }
}