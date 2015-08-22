using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public abstract class ShootingSprite : Sprite
    {
        private string m_SerialNumber;

        public ShootingSprite(Game i_Game, string i_AssetName)
            : base(i_AssetName, i_Game)
        {
            m_SerialNumber = Guid.NewGuid().ToString();
        }

        public string SerialNumber 
        { 
            get
            {
                return m_SerialNumber;
            }
        }

        protected virtual Bullet getAndShootBullet(Color i_BulletColor, float i_BulletVelocity)
        {
            Bullet bullet = null;

            bullet = SpritesFactory.CreateSprite(this.Game, SpritesFactory.eSpriteType.Bullet) as Bullet;
            bullet.Initialize();
            bullet.TintColor = i_BulletColor;
            bullet.GunSerialNumber = m_SerialNumber;
            bullet.ShootingSpriteType = this.GetType();
            bullet.Velocity = new Vector2(0, i_BulletVelocity);

            return bullet;
        }
    }
}
