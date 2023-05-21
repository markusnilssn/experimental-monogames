using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IndieGame.Common
{
    public class UIElement : UIContainer
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }

        public Vector2 Scale { get; set; }
        public Vector2 Position => new Vector2(Left, Top);
        public Vector2 Size => new Vector2(Right - Left, Bottom - Top);

        public Texture2D MainTexture { get; set; }

        public override void Update(GameTime aGameTime) { base.Update(aGameTime); }
        public override void Render(SpriteBatch aRenderer) 
        {
            if (IsEnabled)
                aRenderer.Draw(MainTexture, Position, Color.White);    
                
            base.Render(aRenderer);
        }

        public static UIElement CreateElement(Vector2 aPosition, Vector2 aSize, Vector2 aScale)
        {
            throw new Exception();
        }
    }
}
