using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.Managers;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Screens
{
   public class GamingScreen : GameScreen
    {
       private bool v_IsMouseMoveEnable = true;

        public GamingScreen(Game i_Game):base(i_Game)
       {
           //this.SpritesSortMode = SpriteSortMode.Immediate;
           //this.BlendState = BlendState.AlphaBlend;
       }

        public override void Initialize()
        {
            CollisionsManager collisionsManager = new CollisionsManager(this.Game);
            SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.MotherShip);
            SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.SpaceBackground);
            EnemiesMatrix enemiesMatrix = new EnemiesMatrix(this);
            BarrierGroup barrierGroup = new BarrierGroup(this);
            ConfSpaceShip player1SpaceShipConf = new ConfSpaceShip(PlayerSpaceInvaders.eSpaceShipType.Green, Keys.Left, Keys.Right, new Keys[] { Keys.Enter, Keys.RightControl, Keys.LeftControl }, v_IsMouseMoveEnable);
            ConfSpaceShip player2SpaceShipConf = new ConfSpaceShip(PlayerSpaceInvaders.eSpaceShipType.Blue, Keys.A, Keys.D, Keys.W, !v_IsMouseMoveEnable);
            SpaceInvadersServices.CreateNewPlayers(this, player1SpaceShipConf, player2SpaceShipConf);
            SpaceInvadersServices.ChangeBarriersGroupVerticalPosition(this, barrierGroup);
            this.Add(enemiesMatrix);
            this.Add(barrierGroup);
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            bool isGameOver = SpaceInvadersServices.IsAllPlayersLost(this.Game);
            if (isGameOver)
            {
                SpaceInvadersServices.GameOver(this.Game);
            }
            base.Update(gameTime);
        }
 

    }
}
