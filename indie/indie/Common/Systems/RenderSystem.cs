using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public class RenderSystem : System
    {
        public class Statistics
        {
            public int EntitiesRendered { get; set; } = 0;
        }

        private Statistics m_Statistics = new Statistics();

        public RenderSystem(Engine engine)
            : base(engine)
        {
            RequireComponent(typeof(Sprite));
            RequireComponent(typeof(Transform));
        }

        public override void Update(GameTime aGameTime) { }
        public override void Render(SpriteBatch aRenderer)
        {
            foreach (Entity entity in m_Entities)
            {
                Debug.Assert(entity != null, $"Entity {entity.ToString()} is null.");

                Sprite sprite = m_Engine.GetComponent<Sprite>(entity);
                Transform transform = m_Engine.GetComponent<Transform>(entity);

                aRenderer.Draw(
                    sprite.Texture,
                    transform.Position,
                    null,
                    sprite.Color,
                    transform.Rotation,
                    sprite.Origin,
                    transform.Scale,
                    sprite.Effects,
                    sprite.LayerDepth);

                m_Statistics.EntitiesRendered++;
            }

            m_Statistics.EntitiesRendered = 0;
        }
    }
}