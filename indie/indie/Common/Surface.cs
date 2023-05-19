using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IndieGame.Common.Surface;

namespace IndieGame.Common
{
    public class Surface
    {
        public enum Depth
        {
            BACK = 0,
            MIDDLE,
            FRONT,
        }

        public abstract class Layer
        {
            private Surface m_Surface;
            private Depth m_Depth;

            public Layer(Surface surface, Depth depth)
            {
                m_Surface = surface;
                m_Depth = depth;
            }

            public virtual void Start() { }
            public virtual void Destroy() { }
            
            public virtual void Update(GameTime aGameTime) { }
            public virtual void Render(SpriteBatch aRenderer) { }

            public Depth Depth => m_Depth;
        }

        private Dictionary<Depth, List<Layer>> m_Layers;
        private List<int> m_HashCodes;

        public Surface()
        {
            m_Layers = new Dictionary<Depth, List<Layer>>();
            int length = Enum.GetNames(typeof(Depth)).Length;
            for(int i = 0; i < length; i++)
            {
                m_Layers.Add((Depth)i, new List<Layer>());
            }

            m_HashCodes = new List<int>(); // Currently existing hash codes whithin collection "m_Layer"
        }

        public virtual void Start() { }
        public virtual void Destroy() { }

        public virtual void Update(GameTime aGameTime) 
        {
            foreach (var (depth, layers) in m_Layers)
            {
                foreach (var layer in layers)
                {
                    layer.Update(aGameTime);
                }
            }
        }

        public virtual void Render(SpriteBatch aRenderer) {
            
            foreach (var (depth, layers) in m_Layers)
            {
                foreach (var layer in layers)
                {
                    layer.Render(aRenderer);
                }
            }
        }
  
        public void PopLayer(Layer aLayer)
        {
            var type = aLayer.GetType();
            var hashCode = type.GetHashCode();
            bool contains = m_HashCodes.Contains(hashCode);
            if (!contains)
            {
                throw new Exception("Layer not found");
            }
            m_Layers[aLayer.Depth].Remove(aLayer);
            m_HashCodes.Remove(hashCode);
        }
    }
}
