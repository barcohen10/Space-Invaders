using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Services;
using  Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex02Dotan301810610Bar308000322.Services
{
    public static class TextServices
    {
        public static void CreateAndAdjustTexts(GameScreen i_GameScreen,List<string> i_Texts,SpritesFactory.eSpriteType i_SpriteType,float i_PositionX,float i_StartPositionY)
        {
            Text spriteText = null;
            float dynamicPositionY = i_StartPositionY;
            foreach (string text in i_Texts)
            {
               spriteText =  SpritesFactory.CreateSprite(i_GameScreen, i_SpriteType) as Text;
                if (spriteText != null)
                {
                    spriteText.Position =new Vector2(i_PositionX, dynamicPositionY);
                    spriteText.TextString = text;
                    dynamicPositionY += spriteText.Height;
                }
            }
        }
    }
}
 