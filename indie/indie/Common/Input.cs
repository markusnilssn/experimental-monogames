using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public sealed class Input
    {
        private static MouseState MouseState => Mouse.GetState();
        private static KeyboardState m_KeyboardState;
        private static KeyboardState m_PreviousKeyboardState;

        private Input() { }

        public static bool IsKeyDown(Keys aKey)
        {
            return m_KeyboardState.IsKeyDown(aKey);
        }

        public static bool IsKeyUp(Keys aKey)
        {
            return m_KeyboardState.IsKeyUp(aKey);
        }

        public static bool IsKeyPressed(Keys aKey)
        {
            return m_KeyboardState.IsKeyDown(aKey) && !m_PreviousKeyboardState.IsKeyDown(aKey);
        }

        public static void Update()
        {
            m_PreviousKeyboardState = m_KeyboardState;
            m_KeyboardState = Keyboard.GetState();
        }

        public static int HorizontalAxis
        {
            get
            {
                if (IsKeyDown(Keys.A))
                {
                    return -1;
                }
                else if (IsKeyDown(Keys.D))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public static int VerticalAxis
        {
            get
            {
                if (IsKeyDown(Keys.W))
                {
                    return -1;
                }
                else if (IsKeyDown(Keys.S))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
