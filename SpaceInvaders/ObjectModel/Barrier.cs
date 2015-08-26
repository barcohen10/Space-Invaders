using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using SpaceInvaders.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Infrastructure.ServiceInterfaces;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.ObjectModel
{
    public class Barrier : Sprite, ICollidable2D
    {
        private readonly float r_BarrierVelocity = float.Parse(ConfigurationManager.AppSettings["Barrier.Velocity"].ToString());

        private float m_JumpingVelocity;
        private readonly CollisionServices m_CollisionServices;

        public Barrier(GameScreen i_GameScreen, string i_AssetName)
            : base(i_AssetName, i_GameScreen)
        {
            m_CollisionServices = this.Game.Services.GetService(typeof(CollisionServices)) as CollisionServices;
        }

        public event EventHandler TouchScreenLimit;

        protected virtual void OnTouchScreenLimit()
        {
            if (TouchScreenLimit != null)
            {
                TouchScreenLimit(this, EventArgs.Empty);
            }
        }
        public override void Initialize()
        {
            base.Initialize();
            m_JumpingVelocity = r_BarrierVelocity;
            UseOwnSpriteBatch(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

     
                this.Position = new Vector2(MathHelper.Clamp(this.Position.X, 0, this.Game.GraphicsDevice.Viewport.Width - this.Width), Position.Y);
                if (this.Position.X == 0 || this.Position.X == (this.Game.GraphicsDevice.Viewport.Width - this.Width))
                {
                    OnTouchScreenLimit();
                }
        }
        public void SpeedUp(float i_Velocity)
        {
            m_JumpingVelocity += i_Velocity;
            this.Velocity = new Vector2(m_JumpingVelocity, 0);
        }
        public float CurrentSpeed { get { return m_JumpingVelocity; } }
        public void ChangeToDefaultJumpingSpeed()
        {
            m_JumpingVelocity = r_BarrierVelocity;
        }
        public void StartJumping()
        {
            this.Velocity = new Vector2(m_JumpingVelocity, 0);

        }
        public void StopJumping()
        {
            this.Velocity = new Vector2();

        }
        public override void Collided(ICollidable i_Collidable)
        {
            Bullet bullet = i_Collidable as Bullet;
            Enemy enemy = i_Collidable as Enemy;
            Sprite collidableSprite = i_Collidable as Sprite;
            List<Vector2> collidedPoints;
            bool autoPixelClear = true;
            if (bullet != null)
            {
                CollisionServices.eCollisionDirection collisionDirection = m_CollisionServices.GetCollisionDirection(this, collidableSprite);
                int halfBulletHeight = (int)(bullet.Height * 0.55);
                if (m_CollisionServices.IsPixelsIntersect(this, collidableSprite, out collidedPoints, !autoPixelClear))
                {
                    Sprite barrier = this as Sprite;
                    m_CollisionServices.ClearPixelsInVerticalDirection(ref barrier, collidedPoints, collisionDirection, halfBulletHeight);
                    this.GameScreen.Remove(bullet);
                    bullet.Dispose();
                }
            }

            if (enemy != null)
            {
                m_CollisionServices.IsPixelsIntersect(this, collidableSprite, out collidedPoints, autoPixelClear);
            }
        }



    }
}
