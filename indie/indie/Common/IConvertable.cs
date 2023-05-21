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
            private Dictionary<string, (Type, object)> m_Data;
            private List<MetaData> m_Children;

            public MetaData()
            {
                m_Children = new List<MetaData>();
                m_Data = new Dictionary<string, (Type, object)>();
            }

            public void Push<T>(string akey, T value)
            {
                Type type = typeof(T);
                m_Data.Add(akey, (type, value));
            }

            public void Push(string akey, MetaData aValue)
            {
                m_Children.Add(aValue);
            }

            public string[] GetKeys() => m_Data.Keys.ToArray();
            public object GetValue(string aKey) => m_Data[aKey];

            public MetaData[] GetChildren() => m_Children.ToArray();
        }

        public void Serialize(MetaData aMetaData);
        public MetaData Deseralize();
    }
}
