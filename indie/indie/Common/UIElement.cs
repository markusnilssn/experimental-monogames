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
        public UIContainer Parent { get; private set; }

        public bool IsEnabled { get; set; } = true;
        public string Name { get; set; }

        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }

        public Color Color { get; set; } = Color.White;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Width * Scale.X), (int)(Texture.Height * Scale.Y));
            }
        }

        public override void Update(GameTime aGameTime) { base.Update(aGameTime); }
        public override void Render(SpriteBatch aRenderer)
        {
            if (IsEnabled)
            {
                aRenderer.Draw(Texture, Rectangle, Color);
                //aRenderer.Draw(Texture, Position, Rectangle, Color, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }

            base.Render(aRenderer);
        }

        public static UIElement CreateElement(string aEntryName, string aTexture, Vector2 aPosition, Vector2 aScale, Color aColor)
        {
            UIElement element = new UIElement();
            element.Name = string.IsNullOrEmpty(aEntryName) ? Guid.NewGuid().ToString() : aEntryName;
            element.Texture = ResourceManager.Load<Texture2D>(aTexture);
            element.Position = aPosition;
            element.Scale = aScale;
            element.Color = aColor;

            Rectangle rectangle = new Rectangle((int)aPosition.X, (int)aPosition.Y, (int)(element.Texture.Width * aScale.X), (int)(element.Texture.Height * aScale.Y));

            return element;
        }

        public void SetParent(UIContainer aParent)
        {
            Parent = aParent;
        }
    }
}
