﻿using System;
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

namespace SpaceInvaders.ObjectModel
{
    public class Barrier : Sprite, ICollidable2D
    {
        private readonly float r_BarrierVelocity = float.Parse(ConfigurationManager.AppSettings["Barrier.Velocity"].ToString());

        private readonly CollisionServices m_CollisionServices;

        public Barrier(Game i_Game, string i_AssetName)
            : base(i_AssetName, i_Game)
        {
            this.Velocity = new Vector2(r_BarrierVelocity, 0);
            m_CollisionServices = this.Game.Services.GetService(typeof(CollisionServices)) as CollisionServices;
            UseOwnSpriteBatch(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
        }

        public event EventHandler TouchScreenLimit;

        protected virtual void OnTouchScreenLimit()
        {
            if (TouchScreenLimit != null)
            {
                TouchScreenLimit(this, EventArgs.Empty);
            }
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
                    this.Game.Components.Remove(bullet);
                    bullet.Dispose();
                }
            }
            
            if(enemy != null)
            {
                m_CollisionServices.IsPixelsIntersect(this, collidableSprite, out collidedPoints, autoPixelClear);
            }
        }
    }
}
