using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public class NotImplementedException : Exception
    {
        public NotImplementedException()
        {
        }

        public NotImplementedException(string message)
            : base(message)
        {
        }

        public NotImplementedException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
