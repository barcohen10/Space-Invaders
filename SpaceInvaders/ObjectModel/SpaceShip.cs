using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using SpaceInvaders.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Infrastructure.ServiceInterfaces;
using SpaceInvaders.Services;
using SpaceInvaders.Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModel.Sound;

namespace SpaceInvaders.ObjectModel
{
    public class SpaceShip : ShootingSprite, ICollidable2D, IAnimated, IDieable
    {
        private readonly float r_SpaceShipVelocity = float.Parse(ConfigurationManager.AppSettings["Spaceship.Velocity"].ToString());
        private readonly float r_BulletVelocity = float.Parse(ConfigurationManager.AppSettings["Bullet.Velocity"].ToString());
        private readonly float r_MaxAmountOfBulletsAtOnec = float.Parse(ConfigurationManager.AppSettings["Bullet.MaxAmountOfBulletsAtOnce"].ToString());
        private IInputManager m_InputManager;
        private ButtonState m_LastBTNState = ButtonState.Released;

        public SpaceShip(GameScreen i_GameScreen, string i_AssetName)
            : base(i_GameScreen, i_AssetName)
        {
            m_ShootSound = SoundFactory.CreateSound(this.GameScreen, SoundFactory.eSoundType.LifeDie) as Sound;
        }

        public ConfSpaceShip Configuration { get; set; }

        public override void Initialize()
        {
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            base.Initialize();
            InitAnimations();
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            float y = (float)GraphicsDevice.Viewport.Height;
            y -= Height / 2;
            y -= 30;
            if (Position == Vector2.Zero)
            {
                this.Position = new Vector2(0, y);
            }
            else
            {
                this.Position = new Vector2(this.Position.X, y);
            }
        }
        private bool isAllowedToShoot()
        {
            return !this.isDying
                &&(SpaceInvadersServices.GetShootingSpriteAmountOfAliveBullets(this.GameScreen, this) < r_MaxAmountOfBulletsAtOnec) 
                && ((m_LastBTNState == ButtonState.Pressed && Configuration.IsMouseMovementEnable && m_InputManager.MouseState.LeftButton == ButtonState.Released)
                || (m_InputManager.KeyPressed(Configuration.KeysShoot)));
        }
        public override void Update(GameTime gameTime)
        {
            if (m_InputManager.KeyboardState.IsKeyDown(Configuration.KeyMoveLeft))
            {
                m_Velocity.X = r_SpaceShipVelocity * -1;
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(Configuration.KeyMoveRight))
            {
                m_Velocity.X = r_SpaceShipVelocity;
            }
            else
            {
                m_Velocity.X = 0;
            }

            if (isAllowedToShoot())
            {
                m_ShootSound.Play();
                getAndShootBullet(Color.Red, -r_BulletVelocity);
            }

            m_LastBTNState = m_InputManager.MouseState.LeftButton;
            if (Configuration.IsMouseMovementEnable)
            {
                if (Math.Abs(m_InputManager.MousePositionDelta.X) > 0)
                {
                    float x = m_InputManager.MouseState.Position.X;
                    Position = new Vector2(x, this.Position.Y);
                }
            }

            this.Position = new Vector2(MathHelper.Clamp(this.Position.X, 0, this.Game.GraphicsDevice.Viewport.Width - this.Width), Position.Y);
            base.Update(gameTime);
        }

        protected override Bullet getAndShootBullet(Color i_BulletColor, float i_BulletVelocity)
        {
            Bullet bullet = base.getAndShootBullet(i_BulletColor, i_BulletVelocity);
            bullet.Position = new Vector2(m_Position.X + (Width / 2) - bullet.Width + 3, (m_Position.Y - (bullet.Height / 2)) - 4);
            return bullet;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Enemy enemy = i_Collidable as Enemy;
            if (enemy != null)
            {
                SpaceInvadersServices.GameOver(this.Game);
                this.GameScreen.Remove(enemy);
                enemy.Dispose();
            }

            Bullet bullet = i_Collidable as Bullet;
            if (bullet != null && !isDying)
            {
                if (!bullet.ShootingSpriteType.Name.Equals("SpaceShip"))
                {
                    handleBulletCollision(bullet);
                }
            }
        }

        public void InitAnimations()
        {
            RotateAnimator rotateAnimator = new RotateAnimator(4, RotateAnimator.eDirection.Right, TimeSpan.Zero);
            FadeOutAnimator fadeOutAnimator = new FadeOutAnimator(TimeSpan.FromSeconds(2));
            BlinkAnimator blinkAnimator = new BlinkAnimator("BlinkAnimator", TimeSpan.FromSeconds(0.2), TimeSpan.FromSeconds(2));
            fadeOutAnimator.Finished += fadeOutAnimator_Finished;
            this.Animations.Add(fadeOutAnimator);
            this.Animations.Add(rotateAnimator);
            this.Animations.Add(blinkAnimator);
            this.Animations.Enabled = true;
            this.Animations["FadeOutAnimator"].Pause();
            this.Animations["RotateAnimator"].Pause();
            this.Animations["BlinkAnimator"].Pause();
        }

        private void fadeOutAnimator_Finished(object sender, EventArgs e)
        {
            this.Animations.Pause();
            SpaceInvadersServices.GetPlayerComponent(this.Game, SerialNumber).SpaceShip.Visible = false;
            SpaceInvadersServices.GetPlayerComponent(this.Game, SerialNumber).SpaceShip.Enabled = false;
        }

        private void handleBulletCollision(Bullet i_Bullet)
        {
            PlayerSpaceInvaders player = SpaceInvadersServices.GetPlayerComponent(this.Game, SerialNumber);
            if (player != null)
            {
                player.LoseLife();
                int remainingPlayerLifes = player.Lifes;
                this.Position = new Vector2(0, this.Position.Y);
                if (remainingPlayerLifes == 0)
                {
                    this.isDying = true;
                    LastAnimation();
                }
                else
                {
                    this.Position = new Vector2(0, this.Position.Y);
                    this.Animations["BlinkAnimator"].Reset();
                    this.Animations["BlinkAnimator"].Resume();
                }

                this.GameScreen.Remove(i_Bullet);
                i_Bullet.Dispose();
            }
        }

        public void LastAnimation()
        {
            this.Animations["FadeOutAnimator"].Reset();
            this.Animations["RotateAnimator"].Reset();
            this.Animations["FadeOutAnimator"].Resume();
            this.Animations["RotateAnimator"].Resume();
            this.isDying = true;
        }

        public bool isDying
        {
            get;
            set;
        }
    }
}
