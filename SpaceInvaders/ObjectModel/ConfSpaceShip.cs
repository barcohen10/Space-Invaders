using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.ObjectModel
{
    public class ConfSpaceShip
    {
        public ConfSpaceShip(PlayerSpaceInvaders.eSpaceShipType i_SpaceShipType, Keys i_KeyMoveLeft, Keys i_KeyMoveRight, Keys[] i_KeysShoot, bool i_IsMouseMovementEnable)
        {
            SpaceShipType = i_SpaceShipType;
            KeyMoveLeft = i_KeyMoveLeft;
            KeyMoveRight = i_KeyMoveRight;
            KeysShoot = i_KeysShoot;
            IsMouseMovementEnable = i_IsMouseMovementEnable;
        }

        public ConfSpaceShip(PlayerSpaceInvaders.eSpaceShipType i_SpaceShipType, Keys i_KeyMoveLeft, Keys i_KeyMoveRight, Keys i_KeysShoot, bool i_IsMouseMovementEnable)
        {
            SpaceShipType = i_SpaceShipType;
            KeyMoveLeft = i_KeyMoveLeft;
            KeyMoveRight = i_KeyMoveRight;
            KeysShoot = new Keys[] { i_KeysShoot };
            IsMouseMovementEnable = i_IsMouseMovementEnable;
        }

        public PlayerSpaceInvaders.eSpaceShipType SpaceShipType { get; set; }

        public Keys KeyMoveLeft { get; set; }

        public Keys KeyMoveRight { get; set; }

        public Keys[] KeysShoot { get; set; }

        public bool IsMouseMovementEnable { get; set; }
    }
}
