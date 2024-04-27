using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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

                return Enum.TryParse(color, out Color result) ? result : Color.HotPink;
            }

            Vector4 vector = ParseVector4(aAttribute);

            return new Color(vector.X, vector.Y, vector.Z, vector.W);
        }
    }
}
