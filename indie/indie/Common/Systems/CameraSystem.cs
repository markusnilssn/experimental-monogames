using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace IndieGame.Common
{
    public class CameraSystem : System
    {
        private Entity m_MainCamera;
        private Viewport m_Viewport;

        private float m_ZoomSpeed = 0.1f;
        private float m_Speed = 5.0f;

        public CameraSystem(Engine aEngine, Viewport aViewport)
            : base(aEngine)
        {
            m_Viewport = aViewport;

            RequireComponent(typeof(Transform));
            RequireComponent(typeof(Camera));
        }

        public override void Update(GameTime aGameTime)
        {
            if (m_MainCamera == null)
            {
                if (m_Entities.IsEmpty())
                    return;

                Debug.WriteLine("No main camera attached. Setting first camera in collection as main.");
                m_MainCamera = m_Entities.ElementAt(0);
            }

            Transform transform = m_Engine.GetComponent<Transform>(m_MainCamera);
            Camera camera = m_Engine.GetComponent<Camera>(m_MainCamera);

            float horizontal = Input.HorizontalAxis * m_Speed;
            float vertical = Input.VerticalAxis * m_Speed;

            camera.Position += new Vector2(horizontal, vertical);
            transform.Position = Maff.Lerp(transform.Position, camera.Position, 0.8f);

            if (Input.MouseScrollWheelValue != Input.PreviousMouseScrollWheelValue)
            {
                float delta = Input.MouseScrollWheelValue - Input.PreviousMouseScrollWheelValue;
                delta /= 120.0f;

                float zoom = camera.Zoom + delta * m_ZoomSpeed;
                camera.Zoom = MathHelper.Clamp(zoom, 0.1f, 2.0f);
            }

            float xZoom = Maff.Lerp(transform.Scale.X, camera.Zoom, 0.08f);
            float yZoom = Maff.Lerp(transform.Scale.Y, camera.Zoom, 0.08f);

            transform.Scale = new Vector2(xZoom, yZoom);

            camera.ViewMatrix = Matrix.CreateTranslation(new Vector3(-transform.Position, 0.0f)) *
                Matrix.CreateRotationZ(transform.Rotation) *
                Matrix.CreateScale(new Vector3(transform.Scale.X, transform.Scale.Y, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(m_Viewport.Width / 2.0f, m_Viewport.Height / 2.0f, 0.0f));
        }

        public void SetMainCamera(Entity aEntity)
        {
            m_MainCamera = aEntity;
        }
    }
}
