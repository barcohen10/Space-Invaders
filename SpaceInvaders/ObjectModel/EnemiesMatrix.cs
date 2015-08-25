using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Services;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.ObjectModel
{
    public class EnemiesMatrix : GameComponent
    {
        private const int k_NumOfPinkEnemies = 1, k_NumOfLightBlueEnemies = 2, k_NumOfYellowEnemies = 2;
        private static Random s_RandomGenerator = new Random();
        private readonly List<List<Enemy>> r_EnemiesMatrix;
        private int m_TimeCount;
        private int m_JumpTwiceMilliSec;
        private SpriteJump.eJumpDirection m_JumpDirection;
        private GameScreen m_GameScreen;


        public EnemiesMatrix(GameScreen i_GameScreen)
            : base(i_GameScreen.Game)
        {
            m_TimeCount = 0;
            m_JumpTwiceMilliSec = 1000;
            this.r_EnemiesMatrix = new List<List<Enemy>>();
            m_JumpDirection = SpriteJump.eJumpDirection.Right;
            m_GameScreen = i_GameScreen;
        }

        private Enemy this[int i, int j]
        {
            get
            {
                return r_EnemiesMatrix[i][j];
            }
        }

        public void SpeedUp(double i_Amount)
        {
            m_JumpTwiceMilliSec = (int)(m_JumpTwiceMilliSec * i_Amount);
            foreach (Enemy enemy in GetEnemysAsList())
            {

                enemy.SetChangeShapeSpeed(m_JumpTwiceMilliSec / 2);

            }
        }

        public override void Initialize()
        {
            createEnemiesMatrix(9);
            base.Initialize();
        }

        public override void Update(GameTime i_GameTime)
        {
            bool isSpeedUp = false, isJumpingBackwards = false;
            float originalDistanceToJump;
            int timeToJump;
            originalDistanceToJump = r_EnemiesMatrix[0][0].Width / 2;
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
        private List<Enemy> GetEnemysAsList()
        {
            List<Enemy> enemys = new List<Enemy>();
            foreach (List<Enemy> enemyRow in r_EnemiesMatrix)
            {
                foreach (Enemy enemy in enemyRow)
                {
                    enemys.Add(enemy);
                }
            }
            return enemys;

        }
        private void addEnemiesColumn(int i_NumOfPinkEnemies, int i_NumOfLightBlueEnemies, int i_NumOfYellowEnemies)
        {
            Enemy enemy = null;
            Enemy enemyLastAdded = null;
            SpritesFactory.eSpriteType enemyTypeToCreate = SpritesFactory.eSpriteType.EnemyPink;
            Vector2 position = new Vector2(0, 0);
            int sumOfEnemysInOneColumn = i_NumOfPinkEnemies + i_NumOfLightBlueEnemies + i_NumOfYellowEnemies;
            List<Enemy> currentEnemiesOnMatrix = GetEnemysAsList();
            if (currentEnemiesOnMatrix.Count > 0)
            {
                Enemy enemyFromLastLine = currentEnemiesOnMatrix.Last();
                position = enemyFromLastLine.Position;
                position.X += (float)(enemyFromLastLine.Width * 0.6 + enemyFromLastLine.Width);
            }
            for (int i = 0; i < sumOfEnemysInOneColumn; i++)
            {
                if (i > r_EnemiesMatrix.Count - 1)
                {
                    this.r_EnemiesMatrix.Add(new List<Enemy>());
                }
                enemyTypeToCreate = (i_NumOfPinkEnemies > 0) ? SpritesFactory.eSpriteType.EnemyPink : (i_NumOfLightBlueEnemies > 0 ? SpritesFactory.eSpriteType.EnemyLightBlue : SpritesFactory.eSpriteType.EnemyYellow);
                enemy = SpritesFactory.CreateSprite(m_GameScreen, enemyTypeToCreate) as Enemy;
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
                if (enemyLastAdded != null)
                {

                    if (enemy.TextureStartIndex == enemyLastAdded.TextureStartIndex && enemy.TextureEndIndex == enemyLastAdded.TextureEndIndex)
                    {
                        enemy.ChangeEnemyShape();
                    }
                }
                enemy.Position = new Vector2(position.X, position.Y);
                r_EnemiesMatrix[i].Add(enemy);
                position = new Vector2(position.X, (float)(position.Y + ((enemy.Height * 0.6) + enemy.Width)));
                enemyLastAdded = enemy;
            }
        }

        public void ClearMatrix()
        {
            r_EnemiesMatrix.Clear();
        }
        private void createEnemiesMatrix(int i_NumOfColumns)
        {
            for (int i = 0; i < i_NumOfColumns; i++)
            {
                addEnemiesColumn(k_NumOfPinkEnemies, k_NumOfLightBlueEnemies, k_NumOfYellowEnemies);
            }
        }
        public void AddEnemiesColumn()
        {
            addEnemiesColumn(k_NumOfPinkEnemies, k_NumOfLightBlueEnemies, k_NumOfYellowEnemies);
        }

        private void jumpAllAliveEnemies(SpriteJump.eJumpDirection i_JumpDirection, float i_DistanceToJump, bool i_IsJumpingBackwards)
        {
            foreach (Enemy enemy in GetEnemysAsList())
            {
                if (m_GameScreen.Contains(enemy))
                {
                    enemy.SpriteJump.Jump(i_JumpDirection, i_DistanceToJump, i_IsJumpingBackwards);
                }
            }
        }


        private SpriteJump.SpriteOverJumped getEnemyOverJumpedData()
        {
            SpriteJump.SpriteOverJumped enemyOverJumpedData = new SpriteJump.SpriteOverJumped() { IsTouchedScreenHorizontalBoundary = false };
            foreach (List<Enemy> enemyRow in r_EnemiesMatrix)
            {
                foreach (Enemy enemy in enemyRow)
                {
                    if (enemy.SpriteJump.OverJumpedData.IsTouchedScreenHorizontalBoundary)
                    {
                        enemyOverJumpedData = enemy.SpriteJump.OverJumpedData;
                        enemy.SpriteJump.OverJumpedData = new SpriteJump.SpriteOverJumped() { IsTouchedScreenHorizontalBoundary = false };
                    }
                }
            }

            return enemyOverJumpedData;
        }

        private void randomEnemyShooting()
        {
            int randomNumber = s_RandomGenerator.Next(300);
            int count = 0;
            if (randomNumber <= r_EnemiesMatrix[0].Count)
            {
                for (int i = 0; i < r_EnemiesMatrix.Count; i++)
                {
                    for (int j = 0; j < r_EnemiesMatrix[0].Count; j++)
                    {
                        if (randomNumber == count && m_GameScreen.Contains(r_EnemiesMatrix[i][j]))
                        {
                            r_EnemiesMatrix[i][j].ShootBullet(Color.Blue);
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
            foreach (Enemy enemy in GetEnemysAsList())
            {
                if (m_GameScreen.Contains(enemy))
                {
                    countEnemies++;
                }

                if (m_GameScreen.Contains(enemy) && enemy.SpriteJump.IsTouchedEndOfTheScreen())
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
