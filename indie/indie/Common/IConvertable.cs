using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public interface IConvertable
    {
        public class MetaData
        {
            private Dictionary<string, object> m_Data;
            private List<MetaData> m_Children;

            public MetaData()
            {
                m_Children = new List<MetaData>();
                m_Data = new Dictionary<string, object>();
            }

            public void Push(string akey, string aValue)
            {
                m_Data.Add(akey, aValue);
            }
        }

        public void Serialize(MetaData aMetaData);
        public MetaData Deseralize();
    }
}
