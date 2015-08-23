using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.ObjectModel
{
    public class Text : Sprite
    {
        private SpriteFont m_Font;
        private string m_TextString = string.Empty;

        public Text(GameScreen i_GameScreen, string i_AssetName)
            : base(i_AssetName, i_GameScreen)
        {   
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

    }
}
