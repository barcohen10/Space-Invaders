using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public class MultiPlayerConfiguration
    {
        private ePlayers m_PlayerCount = ePlayers.One;
        private bool v_IsMouseMoveEnable = true;

        public enum ePlayers
        {
            One,
            Two
        }

        public void CreatePlayers(GameScreen i_GameScreen)
        {
            ConfSpaceShip player1SpaceShipConf = new ConfSpaceShip(PlayerSpaceInvaders.eSpaceShipType.Green, Keys.Left, Keys.Right, new Keys[] { Keys.Enter, Keys.RightControl, Keys.LeftControl }, v_IsMouseMoveEnable);
            if (m_PlayerCount.Equals(ePlayers.One))
            {
                SpaceInvadersServices.CreateNewPlayers(i_GameScreen, player1SpaceShipConf);
            }
            else
            {
                ConfSpaceShip player2SpaceShipConf = new ConfSpaceShip(PlayerSpaceInvaders.eSpaceShipType.Blue, Keys.A, Keys.D, Keys.W, !v_IsMouseMoveEnable);
                SpaceInvadersServices.CreateNewPlayers(i_GameScreen, player1SpaceShipConf, player2SpaceShipConf);
            }
        }

        public ePlayers NumberOfPlayers
        {
            get
            {
                return m_PlayerCount;
            }

            set
            {
                m_PlayerCount = value;
            }
        }

        public void ChangeToOnePlayer()
        {
            m_PlayerCount = ePlayers.One;
        }

        public void ChangeToTwoPlayers()
        {
            m_PlayerCount = ePlayers.Two;
        }
    }
}
