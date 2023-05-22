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
        public bool IsEnabled { get; set; }
        public string Name { get; set; }

        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }

        public override void Update(GameTime aGameTime) { base.Update(aGameTime); }
        public override void Render(SpriteBatch aRenderer)
        {
            if (IsEnabled)
                aRenderer.Draw(Texture, Position, Color.White);

            base.Render(aRenderer);
        }

        public static UIElement CreateElement(string texture, Vector2 position, Vector2 scale)
        {
            UIElement element = new UIElement();
            element.Texture = ResourceManager.Load<Texture2D>(texture);
            element.Position = position;
            element.Scale = scale;

            return element;
        }
    }
}
