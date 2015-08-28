﻿using C15Ex03Dotan301810610Bar308000322.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.Managers;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModel.Sound;
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
        private int m_CurrentLevel = 1;
        public GamingScreen(Game i_Game)
            : base(i_Game)
        {
            this.SpritesSortMode = SpriteSortMode.Immediate;
            this.BlendState = BlendState.AlphaBlend;
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
            m_WonLevelSound = SoundFactory.CreateSound(this, SoundFactory.eSoundType.LevelWin);
            m_GameOverSound = SoundFactory.CreateSound(this, SoundFactory.eSoundType.GameOver) ;
            this.Add(enemiesMatrix);
            this.Add(barrierGroup);
            base.Initialize();
            SpaceInvadersServices.ChangeBarriersGroupVerticalPosition(this, barrierGroup);
        }
        private Sound m_WonLevelSound;
        private Sound m_GameOverSound;
        public override void Update(GameTime gameTime)
        {
            if (InputManager.KeyPressed(Keys.P))
            {
                m_ScreensManager.SetCurrentScreen(new PauseScreen(this.Game));
            }
            bool isGameOver = SpaceInvadersServices.IsAllPlayersLost(this.Game);

            if (isGameOver)
            {
                m_GameOverSound.Play();
                SpaceInvadersServices.GameOver(this.Game);
            }
            bool PlayersWon = !SpaceInvadersServices.IsAnyEnemiesLeft(this);
            if (PlayersWon)
            {
                m_WonLevelSound.Play();
                moveLevel();
            }
            base.Update(gameTime);
        }

        private void moveLevel()
        {
            m_CurrentLevel++;

            SpaceInvadersServices.ClearComponents<Enemy>(this);
            SpaceInvadersServices.ClearComponents<Barrier>(this);
            SpaceInvadersServices.ClearComponents<Bullet>(this);
            EnemiesMatrix enemyMatrix = SpaceInvadersServices.GetEnemeiesMatrixComponent(this);
            BarrierGroup barrierGroup = SpaceInvadersServices.GetBarrierGroupComponent(this);
            enemyMatrix.Initialize();
            barrierGroup.Initialize();
            SpaceInvadersServices.ChangeBarriersGroupVerticalPosition(this, barrierGroup);

            int state = m_CurrentLevel % 5;
            if (state >= 2 && state <= 5)
            {
                enemyMatrix.AddEnemiesColumn();
                enemyMatrix.IncraseEnemiesRandomShotting();
                enemyMatrix.AddPointsForEnemyKilling(30);
                barrierGroup.StartJumpingBarriers();
                barrierGroup.Speedup(0.05f);
                if (state == 2)
                {
                    barrierGroup.ChangeToDefaultJumpingSpeed();
                }
            }
            else
            {
                barrierGroup.StopJumpingBarriers();
            }
            this.ScreensManager.SetCurrentScreen(new MoveStageScreen(this.Game, m_CurrentLevel));
        }


    }
}
