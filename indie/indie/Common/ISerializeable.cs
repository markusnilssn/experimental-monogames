using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public interface ISerializeable
    {
        public void Save();
        public void Load();
    }
}
