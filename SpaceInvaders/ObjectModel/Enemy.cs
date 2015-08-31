using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System;
using SpaceInvaders.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Services;
using SpaceInvaders.Infrastructure.ServiceInterfaces;
using SpaceInvaders.Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using C15Ex03Dotan301810610Bar308000322.Services;
using SpaceInvaders.Infrastructure.ObjectModel.Sound;

namespace SpaceInvaders.ObjectModel
{
    public class Enemy : ShootingSprite, ICollidable2D, IAnimated, IDieable
    {
        private readonly float r_BulletVelocity = float.Parse(ConfigurationManager.AppSettings["Bullet.Velocity"].ToString());

        private SpriteJump m_SpriteJump;

        public bool isDying
        {
            get;
            set;
        }

        public int TextureStartIndex
        {
            get
            {
                return r_TextureStartIndex;
            }

            private set
            {
            }
        }

        public int TextureEndIndex
        {
            get
            {
                return r_TextureEndIndex;
            }
        }

        private int r_TextureStartIndex;

        private int r_TextureEndIndex;

        public override void Initialize()
        {
            base.Initialize();

            InitAnimations();
        }

        public void SetChangeShapeSpeed(int i_Miliseconds)
        {
            (this.Animations["CellAnimator"] as CellAnimator).CellTime = TimeSpan.FromMilliseconds(i_Miliseconds);
        }

        public void ChangeEnemyShape()
        {
            (this.Animations["CellAnimator"] as CellAnimator).ChangeShape();
        }

        public event EventHandler TouchedEndOfTheScreen;

        protected virtual void OnTouchedEndOfTheScreen()
        {
            if (TouchedEndOfTheScreen != null)
            {
                TouchedEndOfTheScreen.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            SpaceInvadersServices.GetEnemeiesMatrixComponent(this.GameScreen).Remove(this);
        }

        public void InitAnimations()
        {
            ShrinkAnimator shrinkAnimator = new ShrinkAnimator(TimeSpan.FromSeconds(1.5));
            RotateAnimator rotateAnimator = new RotateAnimator(5, RotateAnimator.eDirection.Right, TimeSpan.FromSeconds(1.5));
            CellAnimator cellAnimator = new CellAnimator(TimeSpan.FromMilliseconds(500), r_TextureStartIndex, r_TextureEndIndex, TimeSpan.Zero);
            this.Animations.Add(shrinkAnimator);
            this.Animations.Add(rotateAnimator);
            this.Animations.Add(cellAnimator);
            cellAnimator.Resume();
            shrinkAnimator.Pause();
            rotateAnimator.Pause();
            this.Animations.Resume();
            shrinkAnimator.Finished += shrinkAnimator_Finished;
        }

        private void shrinkAnimator_Finished(object sender, EventArgs e)
        {
            this.GameScreen.Remove(this);
        }

        public void LastAnimation()
        {
            isDying = true;
            this.Animations["ShrinkAnimator"].Resume();
            this.Animations["RotateAnimator"].Resume();
        }

        public Enemy(GameScreen i_GameScreen, Color i_EnemyColor, int i_TextureStartIndex, int i_TextureEndIndex, string i_AssetName)
            : base(i_GameScreen, i_AssetName)
        {
            m_TintColor = i_EnemyColor;
            this.isDying = false;
            m_SpriteJump = new SpriteJump(this);
            r_TextureStartIndex = i_TextureStartIndex;
            r_TextureEndIndex = i_TextureEndIndex;
            m_ShootSound = SoundFactory.CreateSound(this.GameScreen, SoundFactory.eSoundType.EnemyGunShot) as Sound;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public int Points { get; set; }

        public SpriteJump SpriteJump
        {
            get
            {
                return m_SpriteJump;
            }

            set
            {
                m_SpriteJump = value;
            }
        }

        protected override Bullet getAndShootBullet(Color i_BulletColor, float i_BulletVelocity)
        {
            Bullet bullet = base.getAndShootBullet(i_BulletColor, i_BulletVelocity);
            bullet.Position = new Vector2(m_Position.X + ((Width / 2) - bullet.Width) + 3, m_Position.Y + (this.Height - 3));
            return bullet;
        }

        public void ShootBullet(Color i_Color)
        {
            if (SpaceInvadersServices.GetShootingSpriteAmountOfAliveBullets(this.GameScreen, this) == 0)
            {
                getAndShootBullet(i_Color, r_BulletVelocity);
                m_ShootSound.Play();
            }
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            Height = Width = 32;
            m_SourceRectangle = new Rectangle(0, 0, (int)Width, (int)Height);
        }
    }
}
