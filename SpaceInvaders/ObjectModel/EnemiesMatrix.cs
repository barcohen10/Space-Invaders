using System;
using System.Collections.Generic;
using System.Configuration;
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
        private const int k_NumOfPinkEnemies = 1, k_NumOfLightBlueEnemies = 2, k_NumOfYellowEnemies = 2, k_StartupNumOfColumns = 9;
        private static Random s_RandomGenerator = new Random();
        private readonly List<List<Enemy>> r_EnemiesMatrix;
        private int m_YellowEnemyPoints = int.Parse(ConfigurationManager.AppSettings["Scores.YellowEnemy"].ToString());
        private int m_LightBluePoints = int.Parse(ConfigurationManager.AppSettings["Scores.LightBlueEnemy"].ToString());
        private int m_PinkPoints = int.Parse(ConfigurationManager.AppSettings["Scores.PinkEnemy"].ToString());
        private int m_TimeCount;
        private int m_JumpTwiceMilliSec;
        private SpriteJump.eJumpDirection m_JumpDirection;
        private GameScreen m_GameScreen;
        private int m_NumberOfColumns;
        private int m_RandomNumberToGet = 300;

        public EnemiesMatrix(GameScreen i_GameScreen)
            : base(i_GameScreen.Game)
        {
            m_JumpTwiceMilliSec = 1000;
            this.r_EnemiesMatrix = new List<List<Enemy>>();
            m_JumpDirection = SpriteJump.eJumpDirection.Right;
            m_GameScreen = i_GameScreen;
            m_NumberOfColumns = k_StartupNumOfColumns;
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

        public void AddPointsForEnemyKilling(int i_Points)
        {
            m_LightBluePoints += 30;
            m_YellowEnemyPoints += 30;
            m_PinkPoints += 30;
        }

        public void Clear()
        {
            this.r_EnemiesMatrix.Clear();
        }

        public override void Initialize()
        {
            Clear();
            m_TimeCount = 0;
            m_JumpTwiceMilliSec = 1000;
            createEnemiesMatrix(m_NumberOfColumns);
            base.Initialize();
        }

        public override void Update(GameTime i_GameTime)
        {
            bool isSpeedUp = false, isJumpingBackwards = false;
            float originalDistanceToJump;
            int timeToJump;

            originalDistanceToJump = GetEnemysAsList()[0].Width / 2;
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

        public List<Enemy> GetEnemysAsList()
        {
            List<Enemy> enemies = new List<Enemy>();
            foreach (List<Enemy> enemyRow in r_EnemiesMatrix)
            {
                foreach (Enemy enemy in enemyRow)
                {
                    enemies.Add(enemy);
                }
            }

            return enemies;
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
                position.X += (float)((enemyFromLastLine.Width * 0.6) + enemyFromLastLine.Width);
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
                        enemy.Points = m_PinkPoints;
                        break;
                    case SpritesFactory.eSpriteType.EnemyLightBlue:
                        i_NumOfLightBlueEnemies--;
                        enemy.Points = m_LightBluePoints;

                        break;
                    case SpritesFactory.eSpriteType.EnemyYellow:
                        i_NumOfYellowEnemies--;
                        enemy.Points = m_YellowEnemyPoints;

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
                enemy.TouchedEndOfTheScreen += enemy_TouchedEndOfTheScreen;
                r_EnemiesMatrix[i].Add(enemy);
                position = new Vector2(position.X, (float)(position.Y + ((enemy.Height * 0.6) + enemy.Width)));
                enemyLastAdded = enemy;
            }
        }

        private void enemy_TouchedEndOfTheScreen(object sender, EventArgs e)
        {
            SpaceInvadersServices.GameOver(this.Game);
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
            m_NumberOfColumns++;
            addEnemiesColumn(k_NumOfPinkEnemies, k_NumOfLightBlueEnemies, k_NumOfYellowEnemies);
        }

        private void jumpAllAliveEnemies(SpriteJump.eJumpDirection i_JumpDirection, float i_DistanceToJump, bool i_IsJumpingBackwards)
        {
            foreach (Enemy enemy in GetEnemysAsList())
            {
                if (!enemy.isDying)
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
            int randomNumber = s_RandomGenerator.Next(m_RandomNumberToGet);
            int count = 0;
            if (randomNumber <= r_EnemiesMatrix[0].Count)
            {
                for (int i = 0; i < r_EnemiesMatrix.Count; i++)
                {
                    for (int j = 0; j < r_EnemiesMatrix[0].Count; j++)
                    {
                        if (randomNumber == count)
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

        public void Remove(Enemy i_Enemy)
        {
            foreach (List<Enemy> enemyRow in r_EnemiesMatrix)
            {
                enemyRow.Remove(i_Enemy);
            }
        }

        public bool IsAnyEnemiesLeft()
        {
            bool isAnyLeft = (this.GetEnemysAsList().Count == 0) ? false : true;
            return isAnyLeft;
        }

        public void IncraseEnemiesRandomShotting()
        {
            m_RandomNumberToGet -= (int)(m_RandomNumberToGet / 5);
        }
    }
}
