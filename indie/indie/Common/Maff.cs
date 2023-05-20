using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public static class Maff
    {
        public static float AngleFromVector(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public static Vector2 VectorFromLengthAndAngle(float length, float angle)
        {
            return new Vector2(length * (float)Math.Cos(angle), length * (float)Math.Sin(angle));
        }

        public static Color MultiplyAlpha(Color color)
        {
            return new Color((color.A * color.R) / 255,
                (color.A * color.G) / 255,
                (color.A * color.B) / 255,
                color.A);
        }
    }
}
