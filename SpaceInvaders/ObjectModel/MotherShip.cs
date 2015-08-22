using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Services;
using SpaceInvaders.Infrastructure.ServiceInterfaces;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.ObjectModel
{
    public class MotherShip : Sprite, ICollidable2D, IAnimated, IDieable
    {
        private const int k_RandomNumberToGet = 333;
        private readonly float r_MotherShipVelocity = float.Parse(ConfigurationManager.AppSettings["MotherShip.Velocity"]);

        private Random m_RandomGenerator = new Random();

        public MotherShip(GameScreen i_GameScreen, string i_AssetName)
            : base(i_AssetName, i_GameScreen)
        {
            this.Velocity = new Vector2(r_MotherShipVelocity, 0);
        }

        public override void Initialize()
        {
            base.Initialize();
            InitAnimations();
            this.Visible = false;
            UseOwnSpriteBatch(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
        }

        public int Points
        {
            get;
            set;
        }

        public void InitAnimations()
        {
            ShrinkAnimator shrinkAnimator = new ShrinkAnimator(TimeSpan.FromSeconds(2));
            BlinkAnimator blinkAnimator = new BlinkAnimator(TimeSpan.FromSeconds(0.3), TimeSpan.FromSeconds(2));
            FadeOutAnimator fadeoutAnimator = new FadeOutAnimator(TimeSpan.FromSeconds(2));
            fadeoutAnimator.Finished += fadeoutAnimator_Finished;
            this.Animations.Add(shrinkAnimator);
            this.Animations.Add(blinkAnimator);
            this.Animations.Add(fadeoutAnimator);
            this.Animations.Pause();
        }

        private void fadeoutAnimator_Finished(object sender, EventArgs e)
        {
            this.Animations.Pause();
            this.Velocity = new Vector2(r_MotherShipVelocity, 0);
            isDying = false;
            hideMotherShip();
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            Height = 32;
            Width = 120;
            m_SourceRectangle = new Rectangle(0, 0, (int)Width, (int)Height);
            float x = -this.Width;
            float y = (float)this.Height;
            this.Position = new Vector2(x, y);
        }

        private void startShowingMotherShip()
        {
            InitBounds();
            Visible = true;
        }

        private void hideMotherShip()
        {
            Visible = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Visible)
            {
                if (this.Position.X >= this.GraphicsDevice.Viewport.Width)
                {
                    hideMotherShip();
                }
            }
            else if (!this.isDying)
            {
                int randomNumber = m_RandomGenerator.Next(1000);
                if (randomNumber.Equals(k_RandomNumberToGet))
                {
                    startShowingMotherShip();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void LastAnimation()
        {
            this.Animations.Restart();
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;
            if (bullet != null && !this.isDying)
            {
                this.Velocity = new Vector2(0);
                isDying = true;
                LastAnimation();
                PlayerSpaceInvaders player = SpaceInvadersServices.GetPlayerComponent(this.Game, bullet.GunSerialNumber);
                if (player != null)
                {
                    player.AddScore(Points);
                }

                this.GameScreen.Remove(bullet);
                bullet.Dispose();
            }
        }

        public bool isDying
        {
            get;
            set;
        }



    }
}
