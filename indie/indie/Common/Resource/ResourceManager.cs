using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    using HashCode = Int32;

    public class Resource
    {
        public string Path { get; private set; }
        public object Data { get; private set; }

        public Resource(string aPath, object aData)
        {
            Path = aPath;
            Data = aData;
        }
    }

    public static class ResourceManager
    {
        private static ContentManager m_Content;
        private static Dictionary<HashCode, List<Resource>> m_Resources = new Dictionary<HashCode, List<Resource>>();

        public static void Start(ContentManager aContentManager)
        {
            m_Content = aContentManager;
        }

        public static T Load<T>(string aPath)
        {
            Debug.Assert(m_Content != null, "Content manager is null, did you forget to call ResourceManager.Start()?");
            
            var typecode = typeof(T);
            var hashCode = typecode.GetHashCode();

            bool contains = m_Resources.ContainsKey(hashCode);
            if (!contains)
            {
                m_Resources.Add(hashCode, new List<Resource>());
            }

            Predicate<Resource> predicate = (aResource) => aResource.Path == aPath;
            Resource resource = m_Resources[hashCode].Find(predicate);
            if (resource != null)
            {
                Debug.WriteLine("Cached resource found, returning cached resource.");
                return (T)resource.Data;
            }

            Debug.WriteLine("Cached resource not found, loading new resource.");
            T data = m_Content.Load<T>(aPath);
            m_Resources[hashCode].Add(new Resource(aPath, data));
            return data;
        }

        public static T[] LoadAll<T>(HashSet<string> aPath)
        {
            Debug.Assert(m_Content != null, "Content manager is null, did you forget to call ResourceManager.Start()?");

            T[] returnValue = new T[aPath.Count];

            for (int i = 0; i < aPath.Count; i++)
            {
                var path = aPath.ElementAt(i);

                returnValue[i] = Load<T>(path);
            }

            return returnValue;
        }
    }
}