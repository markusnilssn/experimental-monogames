using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace IndieGame.Common
{
    public class UIContainer
    {
        private List<UIElement> m_Elements;

        public UIContainer()
        {
            m_Elements = new List<UIElement>();
        }

        public void AddElement(UIElement aElement)
        {
            m_Elements.Add(aElement);
        }

        public void RemoveElement(UIElement aElement)
        {
            m_Elements.Remove(aElement);
        }

        public virtual void Update(GameTime aGameTime)
        {
            foreach (var element in m_Elements)
            {
                if (!element.IsEnabled)
                    continue;

                element.Update(aGameTime);
            }
        }
        public virtual void Render(SpriteBatch aRenderer)
        {
            foreach (var element in m_Elements)
            {
                if (!element.IsEnabled)
                    continue;

               element.Render(aRenderer);
            }
        }

        public void Clear()
        {
            m_Elements.Clear();
        }

        public UIElement Query(string aName)
        {
            return m_Elements.FirstOrDefault(e => e.Name == aName);
        }

        public T Query<T>(string aName) where T : UIElement
        {
            return m_Elements.FirstOrDefault(e => e.Name == aName) as T;
        }

        public int ElementCount => m_Elements.Count;
        public UIElement[] GetElements() => m_Elements.ToArray();
    }

    public class UIDocument : UIContainer
    {
        public delegate void DocumentChangedCallback();
        public DocumentChangedCallback OnDocumentChanged;

        private FileSystemWatcher m_Watcher;

        private string m_Name;
        private Vector2 m_Size;

        public UIDocument(string aFilePath)
        {

            ParseDocument(aFilePath);
            WatchFilePath(aFilePath);
        }

        ~UIDocument()
        {

        }

        private void WatchFilePath(string aFilePath)
        {
            var directoryName = Path.GetDirectoryName(aFilePath);
            var fileName = Path.GetFileName(aFilePath);

            m_Watcher = new FileSystemWatcher(directoryName, fileName);

            m_Watcher.NotifyFilter = NotifyFilters.LastWrite;

            m_Watcher.Changed += OnFileChanged;
            m_Watcher.EnableRaisingEvents = true;

            Debug.WriteLine("Watching file: " + fileName);
        }

        private void OnFileChanged(object sender,FileSystemEventArgs e)
        {
            Debug.WriteLine("File Changesd: " + e.FullPath);
            List<UIElement> copyOfList = new List<UIElement>(GetElements());
            Clear();
            bool success = ParseDocument(e.FullPath);
            if (!success)
            {
                Debug.WriteLine("Failed to parse document: " + e.FullPath);
                foreach (var element in copyOfList)
                {
                    AddElement(element);
                }
            }
        }

        private bool ParseDocument(string aPath)
        {
            XmlDocument document = new XmlDocument();
            document.Load(aPath);

            XmlElement xmlDocment = document.DocumentElement;

            m_Name = ParseString(xmlDocment.GetAttribute("name"));
            m_Size = ParseVector2(xmlDocment.GetAttribute("size"));

            // Parse Elements
            XmlNodeList elementNodes = document.SelectNodes("/document/element");
            foreach (XmlNode elementNode in elementNodes)
            {
                if (elementNode is not XmlElement)
                {
                    Debug.WriteLine("Element is not an XmlElement");
                    continue;
                }

                XmlElement xmlElement = elementNode as XmlElement;
                UIElement element = ParseElement(xmlElement);
                AddElement(element);
            }

            return true;
        }

        private UIElement ParseElement(XmlElement aXmlElement)
        {
            Dictionary<string, string> attributes = ParseElementAttributes(aXmlElement);

            string name = ParseString(attributes["name"]);
            string texture = ParseString(attributes["texture"]);
            Vector2 position = ParseVector2(attributes["position"]);
            Color color = ParseColor(attributes["color"]);

            Debug.WriteLine("Element: " + name + " " + texture + " " + position + " " + color);

            return new UIElement();
        }

        private static float ParseFloat(string aAttribute) 
        { 
            if(string.IsNullOrEmpty(aAttribute))
            {
                return 0.0f;
            }

            if(aAttribute.Contains(','))
            {
                throw new Exception("We use dots in our floats, not commas!");
            }
             
            if(aAttribute.Contains('.'))
            {
                aAttribute = aAttribute.Replace('.', ',');
            }

            return float.Parse(aAttribute);
        }
        
        private static int ParseInt(string aAttribute) 
        {
            if (string.IsNullOrEmpty(aAttribute))
            {
                return 0;
            }

            if (aAttribute.Contains(',') || aAttribute.Contains('.'))
            {
                throw new Exception("We don't use commas or dots in our ints!");
            }

            return int.Parse(aAttribute);
        }
        
        private static string ParseString(string aAttribute) 
        {
            return aAttribute;
        }

        private static Vector2 ParseVector2(string aAttribute) 
        {
            if (string.IsNullOrEmpty(aAttribute))
            {
                return Vector2.One;
            }

            aAttribute = aAttribute.Trim('{', '}'); // Remove curly braces
            string[] components = aAttribute.Split(',');

            if (components.Length != 2)
            {
                throw new FormatException("Invalid Vector2 format.");
            }

            float x = ParseFloat(components[0]);
            float y = ParseFloat(components[1]);

            return new Vector2(x, y);
        }
        private static Vector3 ParseVector3(string aAttribute) 
        {
            if (string.IsNullOrEmpty(aAttribute))
            {
                return Vector3.One;
            }

            aAttribute = aAttribute.Trim('{', '}'); // Remove curly braces
            string[] components = aAttribute.Split(',');

            if (components.Length != 3)
            {
                throw new FormatException("Invalid Vector3 format.");
            }

            float x = ParseFloat(components[0]);
            float y = ParseFloat(components[1]);
            float z = ParseFloat(components[2]);

            return new Vector3(x, y, z);
        }
        private static Vector4 ParseVector4(string aAttribute) 
        {
            if (string.IsNullOrEmpty(aAttribute))
            {
                return Vector4.One;
            }

            aAttribute = aAttribute.Trim('{', '}'); // Remove curly braces
            string[] components = aAttribute.Split(',');

            if (components.Length != 4)
            {
                throw new FormatException("Invalid Vector4 format.");
            }

            float x = ParseFloat(components[0]);
            float y = ParseFloat(components[1]);
            float z = ParseFloat(components[2]);
            float w = ParseFloat(components[3]);

            return new Vector4(x, y, z, w);
        }
        private static Color ParseColor(string aAttribute)
        {
            if (string.IsNullOrEmpty(aAttribute))
            {
                return Color.HotPink; // ;) 
            }

            Vector4 vector = ParseVector4(aAttribute);

            return new Color(vector.X, vector.Y, vector.Z, vector.W);
        }

        private static Dictionary<string, string> ParseElementAttributes(XmlElement element)
        {
            if (!element.HasAttributes)
                return new Dictionary<string, string>();

            Dictionary<string, string> attributes = new Dictionary<string, string>();
            foreach (XmlAttribute attribute in element.Attributes)
            {
                attributes.Add(attribute.Name, attribute.Value);
            }
            
            return attributes;
        }
    }
}
