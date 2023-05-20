using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public class State
    {
        public State() { }
        public void Update(GameTime aGameTime) { }
        public void Render(SpriteBatch aRenderer) { }
    }

    public class StateStack
    {
        private Stack<State> m_Stack;

        public StateStack() 
        {
            m_Stack = new Stack<State>();
        }

        public void Update(GameTime aGameTime)
        {
            var state = m_Stack.Peek();

            state.Update(aGameTime);
        }

        public void Render(SpriteBatch aRenderer)
        {
            var state = m_Stack.Peek();

            state.Render(aRenderer);
        }

        public void Push(State aState)
        {
            m_Stack.Push(aState);
        }

        public void Pop()
        {
            m_Stack.Pop();
        }

        public void Clear()
        {
            m_Stack.Clear();
        }


    }
}
