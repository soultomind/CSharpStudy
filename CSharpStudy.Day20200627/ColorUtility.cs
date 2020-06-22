using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudy.Day20200627
{
    class ColorUtility
    {
        public static Color FromArgb(int argb)
        {
            int alapha = (argb >> 24) & 0xFF;
            int red = (argb >> 16) & 0xFF;
            int green = (argb >> 8) & 0xFF;
            int blue = argb & 0xFF;
            return Color.FromArgb(alapha, red, green, blue); ;
        }

        public static int ToCssRgba(string argb)
        {
            if (argb == null)
            {
                throw new ArgumentNullException("argb");
            }

            if (argb.Length == 0)
            {
                throw new ArgumentException("argb.Length is zero");
            }

            if (!argb.StartsWith("rgba("))
            {
                throw new ArgumentException("argb is invalid value, usage is rgba(255,0,0,0)");
            }

            if (!argb.EndsWith(")"))
            {
                throw new ArgumentException("argb is invalid value, usage is rgba(255,0,0,0)");
            }

            argb = argb.Substring(5, argb.Length - 6);
            string[] arrArgb = argb.Split(',');
            if (arrArgb.Length != 4)
            {
                throw new ArgumentException("argb is invalid value, usage is rgba(255,0,0,0)");
            }

            int red = int.Parse(arrArgb[0]);
            int green = int.Parse(arrArgb[1]);
            int blue = int.Parse(arrArgb[2]);
            int alpha = int.Parse(arrArgb[3]);
            alpha = alpha << 24;
            red = red << 16;
            green = green << 8;
            return alpha + red + green + blue;
        }
    }
}
