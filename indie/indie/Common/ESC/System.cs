using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public class System
    {
        protected HashSet<Entity> m_Entities { get; private set; } = new HashSet<Entity>();
        private HashSet<Type> m_Requirements = new HashSet<Type>();

        protected Engine m_Engine { get; private set; }

        public bool IsEnabled { get; set; } = true;

        public System(Engine aEngine)
        {
            m_Engine = aEngine;
        }

        protected void RequireComponent(Type aType)
        {
            bool contains = m_Requirements.Add(aType);
#if DEBUG
            if (!contains)
            {
                throw new Exception($"Multiple requirements were found for {aType.Name}");
            }
#endif
        }

        public void Register(Entity aEntity)
        {
            m_Entities.Add(aEntity);
        }

        public void Unregister(Entity aEntity)
        {
            bool contains = m_Entities.Contains(aEntity);
            if (!contains)
            {
                throw new Exception("Entity not found in system");
            }

            m_Entities.Remove(aEntity);
        }

        public virtual void Update(GameTime aGameTime) { }
        public virtual void Render(SpriteBatch aRenderer) { }

        public HashSet<Type> Requirements => m_Requirements;
    }
}
