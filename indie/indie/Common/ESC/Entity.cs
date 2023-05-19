using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public sealed class Entity
    {
        public static readonly int MAX_ENTITIES = 1000;

        public int ID { get; private set; }

        public Entity(int aID)
        {
            ID = aID;
        }

        public override string ToString()
        {
            return $"ID: {ID}";
        }
    }
}
