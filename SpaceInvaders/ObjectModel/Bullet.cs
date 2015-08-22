using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Services;
using SpaceInvaders.Infrastructure.ServiceInterfaces;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.ObjectModel
{
    public class Bullet : Sprite, ICollidable2D
    {
        public Bullet(GameScreen i_GameScreen, string i_AssetName)
            : base(i_AssetName, i_GameScreen)
        {
        }

        public Type ShootingSpriteType { get; set; }

        public string GunSerialNumber { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.Position.Y < -this.Height)
            {
                this.GameScreen.Remove(this);
            }

            if (this.Position.Y > GraphicsDevice.Viewport.Height)
            {
                this.GameScreen.Remove(this);
            }
        }

        public override void Collided(ICollidable i_Collidable)
        {
            Enemy enemy = i_Collidable as Enemy;
            Bullet bullet = i_Collidable as Bullet;

            if (enemy != null)
            {
                if (!this.ShootingSpriteType.Name.Equals("Enemy"))
                {
                    if (!enemy.isDying)
                    {
                        Player player = SpaceInvadersServices.GetPlayerComponent(this.Game, this.GunSerialNumber);
                        EnemiesMatrix enemiesMatrix = SpaceInvadersServices.GetEnemeiesMatrixComponent(this.GameScreen);
                        enemiesMatrix.SpeedUp(0.92);
                        enemy.LastAnimation();
                        if (player != null)
                        {
                            player.AddScore(enemy.Points);
                        }

                        enemy.Dispose();
                    }

                    this.GameScreen.Remove(this);
                    this.Dispose();
                }
            }
            else if (bullet != null)
            {
                if(!bullet.ShootingSpriteType.Name.Equals(this.ShootingSpriteType.Name))
                {
                    if (this.ShootingSpriteType.Name.Equals("SpaceShip"))
                    {
                        this.GameScreen.Remove(this);
                        this.Dispose();
                    }
                    else if (this.ShootingSpriteType.Name.Equals("Enemy"))
                    {
                        int randomNumber = new Random().Next(1, 10);
                        if (randomNumber == 1)
                        {
                            this.GameScreen.Remove(this);
                            this.Dispose();
                        }
                    }
                }
            }
        }
    }
}
