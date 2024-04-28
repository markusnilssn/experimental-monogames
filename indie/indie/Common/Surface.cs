using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public Surface Surface { get; set; }

            public virtual void Start() { }
            public virtual void Destroy() { }
            
            public virtual void Update(GameTime aGameTime) { }
            public virtual void Render(SpriteBatch aRenderer) { }
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
        }

        public void AddLayer(Depth aDepth, Layer aLayer)
        {
            m_Layers[aDepth].Add(aLayer);
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

        public virtual void Render(SpriteBatch aRenderer) 
        {
                   
            foreach (var (depth, layers) in m_Layers)
            {
                foreach (var layer in layers)
                {
                    layer.Render(aRenderer);
                }
            }
        }
    }
}
