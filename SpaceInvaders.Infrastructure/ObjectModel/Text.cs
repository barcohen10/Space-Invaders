﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public Text(Game i_Game, string i_AssetName) : base(i_AssetName, i_Game)
        {   
        }

        public string TextString { get { return m_TextString; } set { m_TextString = value; } }

        protected override void LoadTextureOrFont()
        {
            m_Font = this.Game.Content.Load<SpriteFont>(m_AssetName);
        }

        protected override void DrawSpriteBatch()
        {
            m_SpriteBatch.DrawString(m_Font, TextString, this.Position, this.TintColor);
        }

    }
}
