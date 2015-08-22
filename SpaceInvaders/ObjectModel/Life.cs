using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.ObjectModel
{
    public class Life : Sprite
    {
         public Life(GameScreen i_GameScreen, string i_AssetName)
            : base(i_AssetName, i_GameScreen) 
        {
        }


         public override void Initialize()
         {
             Opacity = 0.5f;
             Scales = new Vector2(0.5f);
             UseOwnSpriteBatch(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
             base.Initialize();
         }

         public override void Draw(GameTime i_GameTime)
         {
             base.Draw(i_GameTime);
         }
    }
}
