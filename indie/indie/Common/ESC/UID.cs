using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public class UID
    {
        private static int nextID = 1;
        public int ID { get; private set; }

        public UID()
        {
            ID = nextID;
            nextID++;
        }
    }
}
