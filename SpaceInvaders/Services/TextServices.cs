using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Services;
using Microsoft.Xna.Framework;
using SpaceInvaders.Menus;

namespace SpaceInvaders.Services
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

        public static void CenterTextsOnScreen(GameScreen i_GameScreen, List<Text> i_Texts = null, List<GameMenuItem> i_MenuItems = null)
        {
            float x = (float)i_GameScreen.GraphicsDevice.Viewport.Width / 2;
            float largestWidth = 0;
            if (i_Texts != null)
            {
                foreach (Text text in i_Texts)
                {
                    if (largestWidth < text.Width)
                    {
                        largestWidth = text.Width;
                    }
                }

                x -= largestWidth / 2;
                foreach (Text text in i_Texts)
                {
                    text.Position = new Vector2(x, text.Position.Y);
                }
            }
            else if (i_MenuItems != null)
            {
                foreach (GameMenuItem menuItem in i_MenuItems)
                {
                    if (largestWidth < menuItem.Width)
                    {
                        largestWidth = menuItem.Width;
                    }
                }

                x -= largestWidth / 2;
                foreach (GameMenuItem menuItem in i_MenuItems)
                {
                    menuItem.Position = new Vector2(x, menuItem.Position.Y);
                }
            }
        }
    }
}
