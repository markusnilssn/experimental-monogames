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

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            return new Vector2(Lerp(a.X, b.X, t), Lerp(a.Y, b.Y, t));
        }

        public static float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        public static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
        {
            return new Vector2(Clamp(value.X, min.X, max.X), Clamp(value.Y, min.Y, max.Y));
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            return (float)Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public static float DistanceSquared(Vector2 a, Vector2 b)
        {
            return (float)(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public static Vector2 Normalize(Vector2 value)
        {
            float length = (float)Math.Sqrt(value.X * value.X + value.Y * value.Y);
            return new Vector2(value.X / length, value.Y / length);
        }

        public static Vector2 Rotate(Vector2 value, float angle)
        {
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);
            return new Vector2(value.X * cos - value.Y * sin, value.X * sin + value.Y * cos);
        }

        public static float AngleBetween(Vector2 a, Vector2 b)
        {
            return (float)Math.Atan2(b.Y - a.Y, b.X - a.X);
        }

        public static float AngleBetween(Vector2 a, Vector2 b, Vector2 c)
        {
            return AngleBetween(a, b) - AngleBetween(b, c);
        }

    }
}
