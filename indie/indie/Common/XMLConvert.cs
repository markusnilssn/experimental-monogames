using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IndieGame.Common
{
    public partial class XMLConvert : IConvert
    {
        public void Serialize<T>(string aFilePath, T aData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamWriter writer = new StreamWriter(aFilePath))
            {
                serializer.Serialize(writer, aData);
            }
        }
       
        public T Deseralize<T>(string aFilePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StreamReader reader = new StreamReader(aFilePath))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

    }
}
