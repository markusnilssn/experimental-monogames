using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using IndieGame.Common;
using IndieGame.Indie;

using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System;
using System.Xml;

namespace IndieGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager m_Graphics;
        private SpriteBatch m_SpriteBatch;

        private Surface m_Surface;
        private Engine m_Engine;

        private StateStack m_StateStack;

        private Entity m_MainCamera;
        private Entity m_Entity;

        private int m_WindowWidth;
        private int m_WindowHeight;

        private FrameCounter m_FrameCounter;
        private SpriteFont m_SpriteFont;

        private UIDocument m_Document;

        public Game1()
        {
            m_Graphics = new GraphicsDeviceManager(this);

            m_WindowWidth = 1280;
            m_WindowHeight = 720;

            m_Graphics.PreferredBackBufferWidth = m_WindowWidth;
            m_Graphics.PreferredBackBufferHeight = m_WindowHeight;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ResourceManager.Start(Content);
            m_FrameCounter = new FrameCounter();

            m_StateStack = new StateStack();
            //m_StateStack.Push(new State());

            // Engine
            m_Engine = new Engine();
            m_Engine.RegisterSystem<RenderSystem>();

            // Test Entity
            m_Entity = m_Engine.CreateEntity();

            Transform transform = Transform.CreateTransform(Vector2.Zero, 0.0f, Vector2.One);

            Texture2D texture = ResourceManager.Load<Texture2D>("block");
            Rectangle sourceRectangle = new Rectangle(0, 0, 32, 32);
            Color color = Color.White;
            float layerDepth = 0.0f;
            Sprite sprite = Sprite.CreateSprite(texture, sourceRectangle, color, layerDepth);

            m_Engine.AddComponent(m_Entity, transform);
            m_Engine.AddComponent(m_Entity, sprite);

            // Camera
            m_Engine.RegisterSystem<CameraSystem>();
            
            m_MainCamera = m_Engine.CreateEntity();

            m_Engine.AddComponent(m_MainCamera, Transform.CreateTransform(Vector2.Zero, 0.0f, Vector2.One));
            m_Engine.AddComponent(m_MainCamera, Camera.CreateCamera(new Vector2(m_WindowWidth, m_WindowHeight), 5.0f, new Vector3(1, 1, 0)));

            // Surface
            m_Surface = new Surface();

            m_SpriteFont = ResourceManager.Load<SpriteFont>("default");

            var assetFolder = ResourceManager.GetAssetFolder();
            var filePath = Path.Combine(assetFolder, "WindowLayout.xml");

            m_Document = new UIDocument(filePath);
            Debug.WriteLine("Element Size:"  + m_Document.ElementCount);
            foreach (var item in m_Document.GetElements())
            {
                Debug.WriteLine("item:" + item.Name);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            if(Input.IsKeyPressed(Keys.Escape))
            {
                Debug.WriteLine("Pressed");
                Exit();
            }

            m_Engine.Update(gameTime); 
            m_Surface.Update(gameTime);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            m_FrameCounter.Update(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // draw entities              
            var mainCamera = m_Engine.GetComponent<Camera>(m_MainCamera);
            m_SpriteBatch.Begin(SpriteSortMode.Deferred,
                                BlendState.AlphaBlend,
                                null, null, null, null,
                                mainCamera.ViewMatrix);

            m_Engine.Render(m_SpriteBatch);
            m_SpriteBatch.End();

            // Draw surface
            m_SpriteBatch.Begin();
            var framesPerSeconds = string.Format("FPS: {0}", (int)m_FrameCounter.AverageFramesPerSecond);
            m_SpriteBatch.DrawString(m_SpriteFont, framesPerSeconds, Vector2.Zero, Color.White);

            m_Surface.Render(m_SpriteBatch);
            m_SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}