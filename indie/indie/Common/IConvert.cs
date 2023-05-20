using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public interface IConvert
    {
        public enum Type
        {
            XML,
            JSON,
        }

        public void Serialize<T>(string aFilePath, T aData);
        public T Deseralize<T>(string aFilePath);
    }
}
