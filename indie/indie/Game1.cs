using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using IndieGame.Common;
using System.Collections.Generic;
using System.IO;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace IndieGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager m_Graphics;
        private SpriteBatch m_SpriteBatch;

        private Surface m_Surface;
        private Engine m_Engine;

        private Entity m_MainCamera;
        private Entity m_Entity;

        private int m_WindowWidth;
        private int m_WindowHeight;

        private FrameCounter m_FrameCounter;
        private int m_FrameCount;
        private SpriteFont m_SpriteFont;

        private ResourceManager m_ResourceManager;

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
            m_ResourceManager = new ResourceManager(Content);
            m_FrameCounter = new FrameCounter();

            // Engine
            m_Engine = new Engine();
            m_Engine.RegisterSystem<RenderSystem>();

            // Test Entity
            m_Entity = m_Engine.CreateEntity();

            Transform transform = Transform.CreateTransform(Vector2.Zero, 0.0f, Vector2.One);

            Texture2D texture = m_ResourceManager.Load<Texture2D>("block");
            Rectangle sourceRectangle = new Rectangle(0, 0, 32, 32);
            Color color = Color.White;
            float layerDepth = 0.0f;
            Sprite sprite = Sprite.CreateSprite(texture, sourceRectangle, color, layerDepth);

            m_Engine.AddComponent(m_Entity, sprite);
            m_Engine.AddComponent(m_Entity, transform );

            // Camera
            m_Engine.RegisterSystem<CameraSystem>();
            
            m_MainCamera = m_Engine.CreateEntity();

            m_Engine.AddComponent(m_MainCamera, Transform.CreateTransform(Vector2.Zero, 0.0f, Vector2.One));
            m_Engine.AddComponent(m_MainCamera, Camera.CreateCamera(new Vector2(m_WindowWidth, m_WindowHeight), 5.0f, new Vector3(1, 1, 0)));

            // Surface
            m_Surface = new Surface();

            m_SpriteFont = m_ResourceManager.Load<SpriteFont>("default");

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
            m_SpriteBatch.Begin(
                SpriteSortMode.Deferred,
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