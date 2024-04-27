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
        public void AddElements(List<UIElement> aElement)
        {
            m_Elements.AddRange(aElement);
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
        private class AttributeTable
        {
            public Dictionary<string, string> collection = new Dictionary<string, string>();

            public void Add(string aKey, string aValue)
            {
                collection.Add(aKey, aValue);
            }

            public string Get(string aKey)
            {
                bool contains = collection.ContainsKey(aKey);
                if (!contains)
                {
                    return null;
                }

                return collection[aKey];
            }

            // override operator []
            public string this[string aKey]
            {
                get => Get(aKey);
                set => Add(aKey, value);
            }
        }

        public delegate void DocumentChangedCallback();
        public DocumentChangedCallback OnDocumentChanged;

        private FileSystemWatcher m_Watcher;

        private string m_Name;
        private Vector2 m_Size;

        private GraphicsDeviceManager m_Graphics;

        public UIDocument(string aFilePath, GraphicsDeviceManager aGraphics)
        {
            m_Graphics = aGraphics;

            ParseDocument(aFilePath);
            WatchFilePath(aFilePath);
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

            // Parse document
            XmlElement xmlDocument = document.DocumentElement;
            m_Name = XMLConvert.ParseString(xmlDocument.GetAttribute("name"));

            try
            {
                m_Size = XMLConvert.ParseVector2(xmlDocument.GetAttribute("size"));
            }
            catch (Exception e)
            {
                void ParseComponent(string aComponent, out float aResult)
                {
                    if (!float.TryParse(aComponent, out aResult))
                    {
                        Debug.WriteLine("Failed to parse component: " + aComponent);
                        aResult = 0;
                    }

                    if(aComponent.Contains("window"))
                    {
                        var split = aComponent.Split('.');
                        if (split.Length != 2)
                        {
                            Debug.WriteLine("Failed to parse window: " + aComponent);
                            return;
                        }

                        var resolution = split[1];
                        if(resolution == "width")
                        {
                            aResult = m_Graphics.PreferredBackBufferWidth;
                        }
                        else if (resolution == "height")
                        {
                            aResult = m_Graphics.PreferredBackBufferHeight;
                        }
                    } 
                    else if (aComponent.Contains("parent"))
                    {

                    } 
                }

                var attribute = xmlDocument.GetAttribute("size");
                string[] components = attribute.Split(',');

                if (components.Length != 2)
                {
                    Debug.WriteLine("Failed to parse size: " + attribute);
                    return false;
                }

                ParseComponent(components[0], out float x);
                ParseComponent(components[1], out float y);
            }

            // Parse Elements

            // RECURSIVE PLEASE
            XmlNodeList elementNodes = document.SelectNodes("/document/element");

            var elements = ParseElements(elementNodes);

            AddElements(elements);

 

            return true;
        }


        private List<UIElement> ParseElements(XmlNodeList aElementNodes)
        {
            if (aElementNodes == null)
                return null;

            List<UIElement> returnValue = new List<UIElement>();
            foreach (XmlNode elementNode in aElementNodes)
            {
                if (elementNode is not XmlElement xmlElement)
                {
                    Debug.WriteLine("Element is not an XmlElement");
                    continue;
                }

                UIElement element = CreateElement(xmlElement);
                if(element == null)
                {
                    Debug.WriteLine("Failed to create element");
                    continue;
                }

                element.AddElements(ParseElements(elementNode.ChildNodes));
            }

            return returnValue;
        }

        private UIElement CreateElement(XmlElement aXmlElement)
        {
            AttributeTable attributes = ParseElementAttributes(aXmlElement);

            string name = XMLConvert.ParseString(attributes["name"]);
            string texture = XMLConvert.ParseString(attributes["texture"]);
            Vector2 position = XMLConvert.ParseVector2(attributes["position"]);
            Vector2 scale = XMLConvert.ParseVector2(attributes["size"]);
            Color color = XMLConvert.ParseColor(attributes["color"]);

            Debug.WriteLine("Element: " + name + " " + texture + " " + position + " " + color);

            return UIElement.CreateElement(texture, position, scale);
        }

        private static AttributeTable ParseElementAttributes(XmlElement element)
        {
            if (!element.HasAttributes)
                return new AttributeTable();

            AttributeTable attributes = new AttributeTable();
            foreach (XmlAttribute attribute in element.Attributes)
            {
                attributes.Add(attribute.Name, attribute.Value);
            }
            
            return attributes;
        }
    }
}
