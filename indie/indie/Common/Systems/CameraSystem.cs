using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public class CameraSystem : System
    {
        private Entity m_MainCamera;

        public CameraSystem(Engine aEngine)
            : base(aEngine)
        {
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

            float horizontal = Input.HorizontalAxis * camera.Speed;
            float vertical = Input.VerticalAxis * camera.Speed;

            transform.Position += new Vector2(horizontal, vertical);

            camera.ViewMatrix = Matrix.CreateTranslation(new Vector3(-transform.Position, 0.0f)) *
                Matrix.CreateRotationZ(transform.Rotation) *
                Matrix.CreateScale(new Vector3(transform.Scale, 1.0f));
        }

        public void SetMainCamera(Entity aEntity)
        {
            m_MainCamera = aEntity;
        }
    }
}
