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
        private static MouseState m_MouseState;
        private static MouseState m_PreviousMouseState;
        private static KeyboardState m_KeyboardState;
        private static KeyboardState m_PreviousKeyboardState;

        private Input() { }

        public static float MouseScrollWheelValue => m_MouseState.ScrollWheelValue;
        public static float PreviousMouseScrollWheelValue => m_PreviousMouseState.ScrollWheelValue;

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

            m_PreviousMouseState = m_MouseState;
            m_MouseState = Mouse.GetState();
        }

        public static int HorizontalAxis
        {
            get
            {
                if (IsKeyDown(Keys.A) || IsKeyDown(Keys.Left))
                {
                    return -1;
                }
                else if (IsKeyDown(Keys.D) || IsKeyDown(Keys.Right))
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
                if (IsKeyDown(Keys.W) || IsKeyDown(Keys.Up))
                {
                    return -1;
                }
                else if (IsKeyDown(Keys.S) || IsKeyDown(Keys.Down))
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
