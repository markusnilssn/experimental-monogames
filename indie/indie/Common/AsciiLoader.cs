using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework;

namespace IndieGame.Common
{
    public class AsciiLoader
    {
        private Dictionary<Ascii, Texture2D> m_Textures;
        private TaskCompletionSource<bool> m_Source;

        private Texture2D m_SpriteSheet;

        private GraphicsDevice m_GraphicsDevice;

        public AsciiLoader(GraphicsDevice aGraphicsDevice)
        {
            m_GraphicsDevice = aGraphicsDevice;
        }

        public void Load()
        {
            m_Textures = new Dictionary<Ascii, Texture2D>();
            m_Source = new TaskCompletionSource<bool>();

            const int kSpriteSize = 32;

            m_SpriteSheet = ResourceManager.Load<Texture2D>("ascii");

            int row = m_SpriteSheet.Width / kSpriteSize;
            int column = m_SpriteSheet.Height / kSpriteSize;

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    int index = i * row + j;

                    int x = i * kSpriteSize;
                    int y = j * kSpriteSize;

                    Rectangle rectangle = new Rectangle(x, y, kSpriteSize, kSpriteSize);

                    Texture2D texture = new Texture2D(m_GraphicsDevice, rectangle.Width, rectangle.Height);
                    Color[] colors = new Color[rectangle.Width * rectangle.Height];
                    m_SpriteSheet.GetData(0, rectangle, colors, 0, colors.Length);
                    texture.SetData(colors);

                    m_Textures.Add((Ascii)index, texture);
                }
            }
        }
    }
}
