using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IndieGame.Common
{
    public class Sprite : Component
    {
        public Texture2D Texture { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Color Color { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; } = new Vector2(0.0f, 0.0f);

        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; }

        public Sprite(Texture2D aTexture, Rectangle aSourceRectangle, Color aColor, float aLayerDepth)
        {
            Texture = aTexture;
            SourceRectangle = aSourceRectangle;
            Color = aColor;
            LayerDepth = aLayerDepth;
        }

        public static Sprite CreateSprite(Texture2D aTexture, Rectangle aSourceRectangle, Color aColor, float aLayerDepth)
        {
            return new Sprite(aTexture, aSourceRectangle, aColor, aLayerDepth);
        }
    }
}
