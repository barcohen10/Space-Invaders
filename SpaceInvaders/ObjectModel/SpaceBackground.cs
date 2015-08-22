using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Infrastructure.ObjectModels;

namespace SpaceInvaders.ObjectModel
{
    public class SpaceBackground : Sprite
    {
        public SpaceBackground(Game i_Game, string i_AssetName)
            : base(i_AssetName, i_Game)
        {
        }

        protected override void InitBounds()
        {
            base.InitBounds();
            this.DrawOrder = int.MinValue;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            m_SpriteBatch.Draw(Texture, new Rectangle(0, 0, this.GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), m_TintColor);
        }
    }
}
