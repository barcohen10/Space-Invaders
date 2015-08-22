using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C15Ex02Dotan301810610Bar308000322.Screens
{
  public  class PauseScreen :GameScreen
    {
      private Sprite m_MessageSprite;
      public PauseScreen(Game i_Game)
          : base(i_Game)
        {
            m_MessageSprite = SpritesFactory.CreateSprite(this.Game, SpritesFactory.eSpriteType.Text);
            (m_MessageSprite as Text).TextString= "Click R to continue playing";
            this.Add(m_MessageSprite);

        }
      public override void Update(GameTime gameTime)
      {
          base.Update(gameTime);

          if (InputManager.KeyPressed(Microsoft.Xna.Framework.Input.Keys.R))
          {
   //Close screen
          }
  

      }
    }
}
