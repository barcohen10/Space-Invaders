using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Services;
using  Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Services
{
    public static class TextServices
    {
        public static List<Text> GetAndCreateTexts(GameScreen i_GameScreen, List<string> i_Texts, SpritesFactory.eSpriteType i_SpriteType, float i_PositionX, float i_PositionY)
        {
            Text spriteText = null;
            List<Text> texts = new List<Text>();
            float dynamicPositionY = i_PositionY;
            foreach (string text in i_Texts)
            {
               spriteText = SpritesFactory.CreateSprite(i_GameScreen, i_SpriteType) as Text;
                if (spriteText != null)
                {
                    spriteText.Position = new Vector2(i_PositionX, dynamicPositionY);
                    spriteText.TextString = text;
                    dynamicPositionY += spriteText.Height;
                }
                texts.Add(spriteText);
            }

            return texts;
        }

        public static void CenterTextsOnScreen(GameScreen i_GameScreen, List<Text> i_Texts)
        {
            float x = (float)i_GameScreen.GraphicsDevice.Viewport.Width / 2;
            float largestTextWidth = 0;
            foreach(Text text in i_Texts)
            {
                if(largestTextWidth < text.Width)
                {
                    largestTextWidth = text.Width;
                }
            }

            x -= largestTextWidth / 2;
            foreach (Text text in i_Texts)
            {
                text.Position = new Vector2(x, text.Position.Y);
            }
        }
    }
}
 