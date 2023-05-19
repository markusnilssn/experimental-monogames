using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace indiegame.Common
{
    public interface ISystem
    {
        public void Update(GameTime gameTime) { }
        public void Render(SpriteBatch renderer) { }
    }
}
