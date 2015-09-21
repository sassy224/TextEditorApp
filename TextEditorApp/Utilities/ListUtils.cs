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
        public static string[] GetKnownColorNames()
        {
            return System.Enum.GetNames(typeof(KnownColor));
        }

        public static FontFamily[] GetInstalledFontFamilies()
        {
            return new InstalledFontCollection().Families;
        }

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