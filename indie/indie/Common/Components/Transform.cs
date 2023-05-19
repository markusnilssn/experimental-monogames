using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IndieGame.Common
{
    public class Transform : Component
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }

        public Transform(Vector2 aPosition, float aRotation, Vector2 aScale)
        {
            Position = aPosition;
            Rotation = aRotation;
            Scale = aScale;
        }

        public static Transform CreateTransform(Vector2 aPosition, float aRotation, Vector2 aScale)
        {
            return new Transform(aPosition, aRotation, aScale);
        }
    }
}