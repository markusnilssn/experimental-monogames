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
        public float Speed { get; set; }
        public Vector3 Zoom { get; set; }

        public Camera(Vector2 aDimentions, float aSpeed, Vector3 aZoom)
        {
            Dimentions = aDimentions;
            Speed = aSpeed;
            Zoom = aZoom;
        }

        public static Camera CreateCamera(Vector2 aDimentions, float aSpeed, Vector3 aZoom)
        {
            return new Camera(aDimentions, aSpeed, aZoom);
        }
    }
}
