using C15Ex03Dotan301810610Bar308000322.Services;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModel.Sound;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Menu
{
    public class TextMenuItem : GameMenuItem
    {
        private Sound m_MoveScreenSound;

        public TextMenuItem(string i_ItemName, GameScreen i_GameScreen, params MethodKey[] i_Methods)
            : base(i_ItemName, i_GameScreen, Color.White, GameMenuItem.eFontSize.Medium, i_Methods)
        {
            m_MoveScreenSound = SoundFactory.CreateSound(this.m_GameScreen.Game, SoundFactory.eSoundType.MenuMove);
        //To Play It :
            m_MoveScreenSound.Play();
        }
    }
}
