using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Services;

namespace SpaceInvaders.ObjectModel
{
    public class EnemiesMatrix : GameComponent
    {
        private const int k_EnemiesMatrixWidth = 9;
        private const int k_EnemiesMatrixHeight = 5;
        private const int k_NumOfPinkEnemies = 9, k_NumOfLightBlueEnemies = 18, k_NumOfYellowEnemies = 18;
        private static Random s_RandomGenerator = new Random();
        private readonly Enemy[,] r_EnemiesMatrix;
        private int m_TimeCount;
        private int m_JumpTwiceMilliSec;
        private SpriteJump.eJumpDirection m_JumpDirection;

        public EnemiesMatrix(Game i_Game)
            : base(i_Game)
        {
            m_TimeCount = 0;
            m_JumpTwiceMilliSec = 1000;
            this.r_EnemiesMatrix = new Enemy[k_EnemiesMatrixHeight, k_EnemiesMatrixWidth];
            m_JumpDirection = SpriteJump.eJumpDirection.Right;
        }

        public void SpeedUp(double i_Amount)
        {
            m_JumpTwiceMilliSec = (int)(m_JumpTwiceMilliSec * i_Amount);
            foreach (Enemy enemy in r_EnemiesMatrix)
            {
                enemy.SetChangeShapeSpeed(m_JumpTwiceMilliSec / 2);
            }
        }

        public override void Initialize()
        {
            createEnemiesMatrix(k_NumOfPinkEnemies, k_NumOfLightBlueEnemies, k_NumOfYellowEnemies);
            base.Initialize();
        }

        public override void Update(GameTime i_GameTime)
        {
            bool isSpeedUp = false, isJumpingBackwards = false;
            float originalDistanceToJump;
            int timeToJump;
            if (r_EnemiesMatrix[0, 0] != null)
            {
                originalDistanceToJump = r_EnemiesMatrix[0, 0].Width / 2;
                checkIfEnemiesWonOrLost();
                randomEnemyShooting();
                m_TimeCount += i_GameTime.ElapsedGameTime.Milliseconds;
                timeToJump = m_JumpTwiceMilliSec / 2;
                if (m_TimeCount >= timeToJump)
                {
                    jumpAllAliveEnemies(m_JumpDirection, originalDistanceToJump, isJumpingBackwards);
                    if (m_JumpDirection.Equals(SpriteJump.eJumpDirection.DownAndLeft))
                    {
                        m_JumpDirection = SpriteJump.eJumpDirection.Left;
                    }
                    else if (m_JumpDirection.Equals(SpriteJump.eJumpDirection.DownAndRight))
                    {
                        m_JumpDirection = SpriteJump.eJumpDirection.Right;
                    }

                    SpriteJump.SpriteOverJumped enemyOverJumpedData = getEnemyOverJumpedData();
                    if (enemyOverJumpedData.IsTouchedScreenHorizontalBoundary)
                    {
                        switch (m_JumpDirection)
                        {
                            case SpriteJump.eJumpDirection.Right:
                                isJumpingBackwards = true;
                                jumpAllAliveEnemies(enemyOverJumpedData.DirectionToJumpBackwards, enemyOverJumpedData.DistanceToJumpBackwards, isJumpingBackwards);
                                isSpeedUp = true;
                                m_JumpDirection = SpriteJump.eJumpDirection.DownAndLeft;
                                break;
                            case SpriteJump.eJumpDirection.Left:
                                isJumpingBackwards = true;
                                jumpAllAliveEnemies(enemyOverJumpedData.DirectionToJumpBackwards, enemyOverJumpedData.DistanceToJumpBackwards, isJumpingBackwards);
                                isSpeedUp = true;
                                m_JumpDirection = SpriteJump.eJumpDirection.DownAndRight;
                                break;
                        }
                    }

                    m_TimeCount -= timeToJump;
                    if (isSpeedUp)
                    {
                        SpeedUp(0.95);
                    }
                }
            }
        }

        private void createEnemiesMatrix(int i_NumOfPinkEnemies, int i_NumOfLightBlueEnemies, int i_NumOfYellowEnemies)
        {
            Enemy enemy = null;
            int enemiesCount = 1;
            SpritesFactory.eSpriteType enemyTypeToCreate = SpritesFactory.eSpriteType.EnemyPink;
            Vector2 position = new Vector2(0, 0);

            for (int i = 0; i < k_EnemiesMatrixHeight; i++)
            {
                for (int j = 0; j < k_EnemiesMatrixWidth; j++)
                {
                    enemyTypeToCreate = (i_NumOfPinkEnemies > 0) ? SpritesFactory.eSpriteType.EnemyPink : (i_NumOfLightBlueEnemies > 0 ? SpritesFactory.eSpriteType.EnemyLightBlue : SpritesFactory.eSpriteType.EnemyYellow);
                    enemy = SpritesFactory.CreateSprite(this.Game, enemyTypeToCreate) as Enemy;
                    this.r_EnemiesMatrix[i, j] = enemy;
                    switch (enemyTypeToCreate)
                    {
                        case SpritesFactory.eSpriteType.EnemyPink: i_NumOfPinkEnemies--;
                            position.Y = 3 * enemy.Width;
                            break;
                        case SpritesFactory.eSpriteType.EnemyLightBlue:
                            i_NumOfLightBlueEnemies--;
                            break;
                        case SpritesFactory.eSpriteType.EnemyYellow:
                            i_NumOfYellowEnemies--;
                            break;
                    }

                    if (i % 2 == 1)
                    {
                        Enemy lastEnemy = r_EnemiesMatrix[i, 0];
                        if (enemy.TextureStartIndex == lastEnemy.TextureStartIndex && enemy.TextureEndIndex == lastEnemy.TextureEndIndex)
                        {
                            enemy.ChangeEnemyShape();
                        }
                    }

                    enemy.Position = (enemiesCount % 9 != 0) ? new Vector2((float)(position.X + (enemy.Width * 0.6) + enemy.Width), position.Y) : new Vector2(0, position.Y);
                    position = (enemiesCount % 9 != 0) ? enemy.Position : new Vector2(0, (float)(position.Y + ((enemy.Width * 0.6) + enemy.Width)));
                    enemiesCount++;
                }
            }
        }

        private void jumpAllAliveEnemies(SpriteJump.eJumpDirection i_JumpDirection, float i_DistanceToJump, bool i_IsJumpingBackwards)
        {
            for (int i = 0; i < k_EnemiesMatrixHeight; i++)
            {
                for (int j = 0; j < k_EnemiesMatrixWidth; j++)
                {
                    if (this.Game.Components.Contains(r_EnemiesMatrix[i, j]))
                    {
                        r_EnemiesMatrix[i, j].SpriteJump.Jump(i_JumpDirection, i_DistanceToJump, i_IsJumpingBackwards);
                    }
                }
            }
        }

        private SpriteJump.SpriteOverJumped getEnemyOverJumpedData()
        {
            SpriteJump.SpriteOverJumped enemyOverJumpedData = new SpriteJump.SpriteOverJumped() { IsTouchedScreenHorizontalBoundary = false };
            foreach (Enemy enemy in r_EnemiesMatrix)
            {
                if (enemy.SpriteJump.OverJumpedData.IsTouchedScreenHorizontalBoundary)
                {
                    enemyOverJumpedData = enemy.SpriteJump.OverJumpedData;
                    enemy.SpriteJump.OverJumpedData = new SpriteJump.SpriteOverJumped() { IsTouchedScreenHorizontalBoundary = false };
                }
            }

            return enemyOverJumpedData;
        }

        private void randomEnemyShooting()
        {
            int randomNumber = s_RandomGenerator.Next(3000);
            int count = 0;
            if (randomNumber <= r_EnemiesMatrix.Length)
            {
                for (int i = 0; i < k_EnemiesMatrixHeight; i++)
                {
                    for (int j = 0; j < k_EnemiesMatrixWidth; j++)
                    {
                        if (randomNumber == count && this.Game.Components.Contains(r_EnemiesMatrix[i, j]))
                        {
                            r_EnemiesMatrix[i, j].ShootBullet(Color.Blue);
                            break;
                        }

                        count++;
                    }

                    if (randomNumber == count)
                    {
                        break;
                    }
                }
            }
        }

        private void checkIfEnemiesWonOrLost()
        {
            int countEnemies = 0;
            foreach (Enemy enemy in r_EnemiesMatrix)
            {
                if (this.Game.Components.Contains(enemy))
                {
                    countEnemies++;
                }

                if (this.Game.Components.Contains(enemy) && enemy.SpriteJump.IsTouchedEndOfTheScreen())
                {
                    SpaceInvadersServices.GameOver(this.Game);
                    break;
                }
            }

            if (countEnemies == 0)
            {
                SpaceInvadersServices.GameOver(this.Game);
            }
        }
    }
}
