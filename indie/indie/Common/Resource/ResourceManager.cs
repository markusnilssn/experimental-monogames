using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private static GraphicsDevice m_GraphicsDevice;

        private static Dictionary<HashCode, List<Resource>> m_Resources = new Dictionary<HashCode, List<Resource>>();

        public static void Start(ContentManager aContentManager, GraphicsDevice aGraphicsDevice)
        {
            m_Content = aContentManager;
            m_GraphicsDevice = aGraphicsDevice;
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
                return (T)resource.Data;
            }

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

        public static string GetRootDirectory()
        {
            //C:\Users\makiswag\Documents\GitHub\experimental-monogames\indie\indie\bin\Debug\net6.0
            var rootFolder = Environment.CurrentDirectory;
            string searchDirectory = "indie";
            
            string[] directories = rootFolder.Split('\\');

            int count = 0;
            string secondIndieDirectory = null;

            for (int i = 0; i < directories.Length; i++)
            {
                if (directories[i] == searchDirectory)
                {
                    count++;
                    if (count == 2)
                    {
                        secondIndieDirectory = string.Join("\\", directories, 0, i + 1);
                        break;
                    }
                }
            }
            if(secondIndieDirectory == null)
            {
                throw new Exception("Second 'indie' directory not found.");
            }

            return secondIndieDirectory;
        }

        public static string GetAssetFolder() => Path.Combine(GetRootDirectory(), "Assets");
        public static string GetFilePath(string aFilePath) => Path.Combine(GetRootDirectory(), aFilePath);
    }
}