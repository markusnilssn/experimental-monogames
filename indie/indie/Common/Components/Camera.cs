using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public class Camera : Component
    {
        public Matrix ViewMatrix { get; set; }
        public Vector2 Dimentions { get; set; }
        public float Zoom { get; set; }

        public Vector2 Position;

        public Camera(Vector2 aDimentions, float aZoom)
        {
            Dimentions = aDimentions;
            Zoom = aZoom;
        }

        public static Camera CreateCamera(Vector2 aDimentions, float aZoom)
        {
            return new Camera(aDimentions, aZoom);
        }
    }
}
