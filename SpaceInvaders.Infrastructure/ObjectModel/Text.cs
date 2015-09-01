using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Infrastructure.ServiceInterfaces;

namespace SpaceInvaders.Infrastructure.ObjectModel
{
    public class Text : Sprite
    {
        private SpriteFont m_Font;
        private string m_TextString = string.Empty;
        private IInputManager m_InputManager;

        public Text(GameScreen i_GameScreen, string i_AssetName)
            : base(i_AssetName, i_GameScreen)
        {
        }

        public override void Initialize()
        {
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            base.Initialize();
            InitAnimations();
        }

        public override void Update(GameTime gameTime)
        {
            IsMouseHover(m_InputManager);
            base.Update(gameTime);
        }

        public string TextString { get { return m_TextString; } set { m_TextString = value; } }

        protected override void LoadTextureOrFont()
        {
            m_Font = this.Game.Content.Load<SpriteFont>(m_AssetName);
        }

        protected override void DrawSpriteBatch()
        {
            m_SpriteBatch.DrawString(m_Font, TextString, this.Position, this.TintColor, this.Rotation, this.RotationOrigin, this.Scales, SpriteEffects.None, this.LayerDepth);
        }

        public override float Height
        {
            get
            {
                return m_Font.MeasureString(this.TextString).Y;
            }

            set
            {
                base.Height = value;
            }
        }

        public override float Width
        {
            get
            {
                return m_Font.MeasureString(this.TextString).X;
            }

            set
            {
                base.Width = value;
            }
        }

        public void InitAnimations()
        {
            PulseAnimator pulseAnimator = new PulseAnimator("pulse", TimeSpan.Zero, 1.03f, 2);
            this.Animations.Add(pulseAnimator);
            this.Animations.Enabled = true;
            this.Animations["pulse"].Pause();
        }
    }
}
