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

namespace SpaceInvaders
{
    public class SpaceInvaders : Game
    {
        private const int k_ScreenHeight = 640;
        private const int k_ScreenWidth = 800;
        private GraphicsDeviceManager m_Graphics;
        private SpriteBatch m_SpriteBatch;
        private bool v_IsMouseMoveEnable = true;

        public SpaceInvaders()
        {
            m_Graphics = new GraphicsDeviceManager(this);
            m_Graphics.PreferredBackBufferWidth = k_ScreenWidth;
            m_Graphics.PreferredBackBufferHeight = k_ScreenHeight;
            m_Graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(GraphicsDeviceManager), m_Graphics);
            this.Services.AddService(typeof(ContentManager), this.Content);
            this.Services.AddService(typeof(SpriteBatch), m_SpriteBatch);
            this.Services.AddService(typeof(CollisionServices), CollisionServices.Instance);
            IInputManager inputManager = new InputManager(this);
            CollisionsManager collisionsManager = new CollisionsManager(this);
            SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.MotherShip);
            SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.SpaceBackground);
            EnemiesMatrix enemiesMatrix = new EnemiesMatrix(this);
            BarrierGroup barrierGroup = new BarrierGroup(this);
            ConfSpaceShip player1SpaceShipConf = new ConfSpaceShip(PlayerSpaceInvaders.eSpaceShipType.Green, Keys.Left, Keys.Right, new Keys[] { Keys.Enter, Keys.RightControl, Keys.LeftControl }, v_IsMouseMoveEnable);
            ConfSpaceShip player2SpaceShipConf = new ConfSpaceShip(PlayerSpaceInvaders.eSpaceShipType.Blue, Keys.A, Keys.D, Keys.W, !v_IsMouseMoveEnable);
            SpaceInvadersServices.CreateNewPlayers(this, player1SpaceShipConf, player2SpaceShipConf);
            SpaceInvadersServices.ChangeBarriersGroupVerticalPosition(this, barrierGroup);
            this.Components.Add(enemiesMatrix);
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            bool isGameOver = SpaceInvadersServices.IsAllPlayersLost(this);
            if (isGameOver)
            {
                SpaceInvadersServices.GameOver(this);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            m_SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            base.Draw(gameTime);
            m_SpriteBatch.End();
        }
    }
}
