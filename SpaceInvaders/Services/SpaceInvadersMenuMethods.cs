using C15Ex03Dotan301810610Bar308000322.Interfaces;
using C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuScreens;
using C15Ex03Dotan301810610Bar308000322.Screens;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.Managers;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Services
{
    public class SpaceInvadersMenuMethods : GameComponent, ISpaceInvaders, IAction
    {
        private readonly eMethodsToRun r_MethodToRun;
        private Game m_Game;

        public enum eMethodsToRun
        {
            Play,
            Quit,
            OnePlayer,
            TwoPlayers,
            SoundOptions,
            ScreenOptions,
            Done
        }

        public SpaceInvadersMenuMethods(Game i_Game, eMethodsToRun i_MethodToRun)
            : base(i_Game)
        {
            r_MethodToRun = i_MethodToRun;
            m_Game = i_Game;
        }

        void ISpaceInvaders.Play()
        {
            ScreensManager screensManager = SpaceInvadersServices.GetScreensManagerComponent(m_Game);
            screensManager.SetCurrentScreen(new GamingScreen(m_Game));
            screensManager.SetCurrentScreen(new MoveStageScreen(m_Game, 1));
        }
        void ISpaceInvaders.Quit()
        {
            m_Game.Exit();
        }

        void ISpaceInvaders.OnePlayer()
        {
        }

        void ISpaceInvaders.TwoPlayers()
        {
        }
        void ISpaceInvaders.OpenSoundOptionsScreen()
        {
            ScreensManager screensManager = SpaceInvadersServices.GetScreensManagerComponent(m_Game);
            screensManager.SetCurrentScreen(new SoundOptionsScreen(m_Game));
        }
        void ISpaceInvaders.OpenScreenOptionsScreen()
        {
            ScreensManager screensManager = SpaceInvadersServices.GetScreensManagerComponent(m_Game);
            screensManager.SetCurrentScreen(new ScreenOptionsScreen(m_Game));
        }
        void ISpaceInvaders.Done()
        {
            ScreensManager screensManager = SpaceInvadersServices.GetScreensManagerComponent(m_Game);
            GameScreen previousScreen = screensManager.ActiveScreen.PreviousScreen;
            screensManager.Remove(screensManager.ActiveScreen);
            screensManager.SetCurrentScreen(previousScreen);
        }

        public void RunMethod()
        {
            switch (r_MethodToRun)
            {
                case eMethodsToRun.Play:
                    {
                        (this as ISpaceInvaders).Play();
                        break;
                    }
                case eMethodsToRun.Quit:
                    {
                        (this as ISpaceInvaders).Quit();
                        break;
                    }

                case eMethodsToRun.OnePlayer:
                    {
                        (this as ISpaceInvaders).OnePlayer();
                        break;
                    }
                case eMethodsToRun.TwoPlayers:
                    {
                        (this as ISpaceInvaders).TwoPlayers();
                        break;
                    }
                case eMethodsToRun.SoundOptions:
                    {
                        (this as ISpaceInvaders).OpenSoundOptionsScreen();
                        break;
                    }
                case eMethodsToRun.ScreenOptions:
                    {
                        (this as ISpaceInvaders).OpenScreenOptionsScreen();
                        break;
                    }
                case eMethodsToRun.Done:
                    {
                        (this as ISpaceInvaders).Done();
                        break;
                    }
            }
        }
    }

}
