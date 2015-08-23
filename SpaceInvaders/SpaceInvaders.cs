using System;
using SpaceInvaders.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.Managers;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Infrastructure.ServiceInterfaces;
using SpaceInvaders.Services;
using C15Ex02Dotan301810610Bar308000322.Screens;

namespace SpaceInvaders
{
    public class SpaceInvaders : Game
    {
        private const int k_ScreenHeight = 640;
        private const int k_ScreenWidth = 800;
        private GraphicsDeviceManager m_Graphics;
        private SpriteBatch m_SpriteBatch;
        private ScreensManager m_ScreensManager;

        public SpaceInvaders()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            m_Graphics.PreferredBackBufferWidth = k_ScreenWidth;
            m_Graphics.PreferredBackBufferHeight = k_ScreenHeight;
            m_Graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IInputManager inputManager = new InputManager(this);
            m_ScreensManager = new ScreensManager(this);
            m_ScreensManager.SetCurrentScreen(new GamingScreen(this));

        }

        protected override void Initialize()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(GraphicsDeviceManager), m_Graphics);
            this.Services.AddService(typeof(ContentManager), this.Content);
            this.Services.AddService(typeof(SpriteBatch), m_SpriteBatch);
            this.Services.AddService(typeof(CollisionServices), CollisionServices.Instance);
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

         
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
         // m_SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            base.Draw(gameTime);
         //m_SpriteBatch.End();
        }
    }
}
