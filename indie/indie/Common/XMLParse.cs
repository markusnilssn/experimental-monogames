using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public partial class XMLConvert
    {
        public static float ParseFloat(string aAttribute)
        {
            if (string.IsNullOrEmpty(aAttribute))
            {
               return 0.0f;
            }

            if (aAttribute.Contains(','))
            {
                throw new Exception("We use dots in our floats, not commas!");
            }

            if(aAttribute.Contains('f'))
            {
                aAttribute = aAttribute.Replace('f', ' ');
            }

            if(aAttribute.Contains('.'))
            {
                aAttribute = aAttribute.Replace('.', ',');
            }

            bool success = float.TryParse(aAttribute, out float result);
            if (!success)
            {
                throw new FormatException("Invalid float format.");
            }

            return result;
        }

        public static int ParseInt(string aAttribute)
        {
            if (string.IsNullOrEmpty(aAttribute))
            {
                return 0;
            }

            if (aAttribute.Contains(',') || aAttribute.Contains('.'))
            {
                throw new Exception("We don't use commas or dots in our ints!");
            }

            bool success = int.TryParse(aAttribute, out int result);
            if (!success)
            {
                throw new FormatException("Invalid int format.");
            }

            return result;
        }

        public static string ParseString(string aAttribute)
        {
            if(string.IsNullOrEmpty(aAttribute))
            {
                return string.Empty;
            }

            return aAttribute;
        }

        public static bool ParseBool(string aAttribute)
        {
            if (string.IsNullOrEmpty(aAttribute))
            {
                throw new FormatException("Invalid bool format.");
            }

            bool success = bool.TryParse(aAttribute, out bool result);
            if (!success)
            {
                throw new FormatException("Invalid bool format.");
            }

            return result;
        }

        public static Vector2 ParseVector2(string aAttribute)
        {
            if (string.IsNullOrEmpty(aAttribute))
            {
                return Vector2.One;
            }

            aAttribute = aAttribute.Trim('{', '}'); // Remove curly braces
            string[] components = aAttribute.Split(',');

            if (components.Length != 2)
            {
                throw new FormatException("Invalid Vector2 format.");
            }

            float x = ParseFloat(components[0]);
            float y = ParseFloat(components[1]);

            return new Vector2(x, y);
        }
        public static Vector3 ParseVector3(string aAttribute)
        {
            if (string.IsNullOrEmpty(aAttribute))
            {
                return Vector3.One;
            }

            aAttribute = aAttribute.Trim('{', '}'); // Remove curly braces
            string[] components = aAttribute.Split(',');

            if (components.Length != 3)
            {
                throw new FormatException("Invalid Vector3 format.");
            }

            float x = ParseFloat(components[0]);
            float y = ParseFloat(components[1]);
            float z = ParseFloat(components[2]);

            return new Vector3(x, y, z);
        }

        public static Vector4 ParseVector4(string aAttribute)
        {
            if (string.IsNullOrEmpty(aAttribute))
            {
                return Vector4.One;
            }

            aAttribute = aAttribute.Trim('{', '}'); // Remove curly braces
            string[] components = aAttribute.Split(',');

            if (components.Length != 4)
            {
                throw new FormatException("Invalid Vector4 format.");
            }

            float x = ParseFloat(components[0]);
            float y = ParseFloat(components[1]);
            float z = ParseFloat(components[2]);
            float w = ParseFloat(components[3]);

            return new Vector4(x, y, z, w);
        }
        public static Color ParseColor(string aAttribute)
        {
            if (string.IsNullOrEmpty(aAttribute))
            {
                return Color.HotPink; // ;) 
            }

            if (aAttribute.Contains("Color"))
            {
                var components = aAttribute.Split('.');

                var color = components[1];

                return GetColor(color);
            }

            Vector4 vector = ParseVector4(aAttribute);

            return new Color(vector.X, vector.Y, vector.Z, vector.W);
        }

        private static Color GetColor(string aColor)
        {
            switch (aColor)
            {
                case "Black":
                    return Color.Black;
                case "White":
                    return Color.White;
                case "Red":
                    return Color.Red;
                case "Green":
                    return Color.Green;
                case "Blue":
                    return Color.Blue;
                case "Yellow":
                    return Color.Yellow;
                case "Purple":
                    return Color.Purple;
                case "Orange":
                    return Color.Orange;
                case "Pink":
                    return Color.Pink;
                case "Brown":
                    return Color.Brown;
                case "Gray":
                    return Color.Gray;
                case "Cyan":
                    return Color.Cyan;
                case "CornflowerBlue":
                    return Color.CornflowerBlue;
                case "HotPink":
                    return Color.HotPink;
                case "Lime":
                    return Color.Lime;
                case "Teal":
                    return Color.Teal;
                case "Magenta":
                    return Color.Magenta;
                case "Silver":
                    return Color.Silver;
                case "Gold":
                    return Color.Gold;
                case "SkyBlue":
                    return Color.SkyBlue;
                case "Turquoise":
                    return Color.Turquoise;
                case "Violet":
                    return Color.Violet;
                case "Indigo":
                    return Color.Indigo;
                case "Maroon":
                    return Color.Maroon;
                case "Olive":
                    return Color.Olive;
                case "Navy":
                    return Color.Navy;
                case "Aquamarine":
                    return Color.Aquamarine;
                case "Coral":
                    return Color.Coral;
                case "Crimson":
                    return Color.Crimson;
                case "DarkBlue":
                    return Color.DarkBlue;
                case "DarkCyan":
                    return Color.DarkCyan;
                case "DarkGoldenrod":
                    return Color.DarkGoldenrod;
                case "DarkGray":
                    return Color.DarkGray;
                case "DarkGreen":
                    return Color.DarkGreen;
                case "DarkKhaki":
                    return Color.DarkKhaki;
                case "DarkMagenta":
                    return Color.DarkMagenta;
                case "DarkOliveGreen":
                    return Color.DarkOliveGreen;
                case "DarkOrange":
                    return Color.DarkOrange;
                case "DarkOrchid":
                    return Color.DarkOrchid;
                case "DarkRed":
                    return Color.DarkRed;
                case "DarkSalmon":
                    return Color.DarkSalmon;
                case "DarkSeaGreen":
                    return Color.DarkSeaGreen;
                case "DarkSlateBlue":
                    return Color.DarkSlateBlue;
                case "DarkSlateGray":
                    return Color.DarkSlateGray;
                case "DarkTurquoise":
                    return Color.DarkTurquoise;
                case "DarkViolet":
                    return Color.DarkViolet;
                case "DeepPink":
                    return Color.DeepPink;
                case "DeepSkyBlue":
                    return Color.DeepSkyBlue;
                case "DimGray":
                    return Color.DimGray;
                case "DodgerBlue":
                    return Color.DodgerBlue;
                case "Firebrick":
                    return Color.Firebrick;
                case "FloralWhite":
                    return Color.FloralWhite;
                case "ForestGreen":
                    return Color.ForestGreen;
                case "Fuchsia":
                    return Color.Fuchsia;
                case "Gainsboro":
                    return Color.Gainsboro;
                case "GhostWhite":
                    return Color.GhostWhite;
                case "GreenYellow":
                    return Color.GreenYellow;
                case "Honeydew":
                    return Color.Honeydew;
            }
            return Color.White;
        }
    }
}
