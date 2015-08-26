using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpaceInvaders.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModels;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.Managers;

namespace SpaceInvaders.Services
{
    public static class SpaceInvadersServices
    {
        public static void CreateNewPlayers(GameScreen i_GameScreen, params ConfSpaceShip[] i_ConfSpaceShips)
        {
            PlayerSpaceInvaders player = null, beforePlayer = null;
            float xSpaceShip, yLifes = 0, xLifes = (float)i_GameScreen.Game.GraphicsDevice.Viewport.Width - 20;
            for (int i = 0; i < i_ConfSpaceShips.Length; i++)
            {
                string nickname = "P" + (i + 1).ToString();
                player = new PlayerSpaceInvaders(i_GameScreen, nickname, i_ConfSpaceShips[i].SpaceShipType);
                foreach (Life life in player.LifesSprites)
                {
                    life.Position = new Vector2(xLifes, yLifes);
                    xLifes -= 20;
                }

                yLifes += 20;
                xLifes = (float)i_GameScreen.Game.GraphicsDevice.Viewport.Width - 20;
                player.SpaceShip.Configuration = i_ConfSpaceShips[i];
                if (beforePlayer != null)
                {
                    xSpaceShip = beforePlayer.SpaceShip.Position.X + beforePlayer.SpaceShip.Width + (beforePlayer.SpaceShip.Width / 2);
                    player.SpaceShip.Position = new Vector2(xSpaceShip, 0);
                    player.ScoreMessage.Position = new Vector2(beforePlayer.ScoreMessage.Position.X, beforePlayer.ScoreMessage.Position.Y + 20);
                }

                beforePlayer = player;
                i_GameScreen.Game.Components.Add(player);
                player.ScoreMessage.TextString = string.Format("{0} Score: {1}", player.Nickname, player.Score);
            }
        }

        public static bool IsAllPlayersLost(Game i_Game)
        {
            bool isAllPlayersLost = true;
            foreach (GameComponent component in i_Game.Components)
            {
                PlayerSpaceInvaders player = component as PlayerSpaceInvaders;
                if (player != null)
                {
                    if (player.Lifes > 0)
                    {
                        isAllPlayersLost = false;
                        break;
                    }
                }
            }

            return isAllPlayersLost;
        }

        public static int GetShootingSpriteAmountOfAliveBullets(GameScreen i_GameScreen, ShootingSprite i_ShottingSprite)
        {
            int aliveBullets = 0;
            string typeName;
            foreach (GameComponent component in i_GameScreen)
            {
                Bullet bullet = component as Bullet;
                if (bullet != null)
                {
                    if (bullet.GunSerialNumber.Equals(i_ShottingSprite.SerialNumber))
                    {
                        typeName = bullet.ShootingSpriteType.Name;
                        aliveBullets += typeName.Equals(i_ShottingSprite.GetType().Name) ? 1 : 0;
                    }
                }
            }

            return aliveBullets;
        }

        public static PlayerSpaceInvaders GetPlayerComponent(Game i_Game, string i_GunSerialNumber)
        {
            PlayerSpaceInvaders player = null;
            foreach (GameComponent component in i_Game.Components)
            {
                player = component as PlayerSpaceInvaders;
                if (player != null)
                {
                    if (player.SpaceShip.SerialNumber.Equals(i_GunSerialNumber))
                    {
                        break;
                    }
                }
            }

            return player;
        }

        public static EnemiesMatrix GetEnemeiesMatrixComponent(GameScreen i_GameScreen)
        {
            EnemiesMatrix enemiesMatrix = null;
            foreach (GameComponent component in i_GameScreen)
            {
                enemiesMatrix = component as EnemiesMatrix;
                if (enemiesMatrix != null)
                {
                    break;
                }
            }

            return enemiesMatrix;
        }
        public static BarrierGroup GetBarrierGroupComponent(GameScreen i_GameScreen)
        {
            BarrierGroup barrierGroup = null;
            foreach (GameComponent component in i_GameScreen)
            {
                barrierGroup = component as BarrierGroup;
                if (barrierGroup != null)
                {
                    break;
                }
            }
            return barrierGroup;

        }

        public static void GameOver(Game i_Game)
        {
            PlayerSpaceInvaders winningPlayer = getWinningPlayer(i_Game);
            List<PlayerSpaceInvaders> otherPlayers = getAllPlayers(i_Game, winningPlayer);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Game Over!");
            string line = string.Format("{0} Won! {0} score is: {1}", winningPlayer.Nickname, winningPlayer.Score);
            builder.AppendLine(line);
            foreach (PlayerSpaceInvaders player in otherPlayers)
            {
                line = string.Format("{0} Lost! {0} score is: {1}", player.Nickname, player.Score);
                builder.AppendLine(line);
            }

            gameOverMessageBox(i_Game, builder.ToString());
        }

        private static void gameOverMessageBox(Game i_Game, string i_Message)
        {
            DialogResult dialogResult = MessageBox.Show(i_Message);
            if (dialogResult == DialogResult.OK)
            {
                i_Game.Exit();
            }
        }

        private static List<PlayerSpaceInvaders> getAllPlayers(Game i_Game, params PlayerSpaceInvaders[] i_PlayersToIgnore)
        {
            List<PlayerSpaceInvaders> players = new List<PlayerSpaceInvaders>();
            List<PlayerSpaceInvaders> playersToIgnore = i_PlayersToIgnore.ToList<PlayerSpaceInvaders>();
            PlayerSpaceInvaders player;

            foreach (GameComponent component in i_Game.Components)
            {
                player = component as PlayerSpaceInvaders;
                if (player != null)
                {
                    if (!playersToIgnore.Contains(player))
                    {
                        players.Add(player);
                    }
                }
            }

            return players;
        }

        private static PlayerSpaceInvaders getWinningPlayer(Game i_Game)
        {
            int maxScore = -1;
            PlayerSpaceInvaders winningPlayer = null;
            List<PlayerSpaceInvaders> allPlayers = getAllPlayers(i_Game);
            foreach (PlayerSpaceInvaders player in allPlayers)
            {
                if (player.Score > maxScore)
                {
                    maxScore = player.Score;
                    winningPlayer = player;
                }
            }

            return winningPlayer;
        }

        public static void ChangeBarriersGroupVerticalPosition(GameScreen i_GameScreen, BarrierGroup i_BarrierGroup)
        {
            SpaceShip spaceShip = null;
            List<PlayerSpaceInvaders> allPlayers = getAllPlayers(i_GameScreen.Game);
            float y;
            if (allPlayers.Count > 0)
            {
                spaceShip = allPlayers[0].SpaceShip;
                y = spaceShip.Position.Y - (spaceShip.Width * 2);
                i_BarrierGroup.ChangeGroupPositionY(y);
            }
        }

        public static bool IsAnyEnemiesLeft(GameScreen i_GameScreen)
        {
            return GetEnemeiesMatrixComponent(i_GameScreen).IsAnyEnemiesLeft();
        }

        public static ScreensManager GetScreensManagerComponent(Game i_Game)
        {
            ScreensManager screensManager = null;
            foreach (GameComponent component in i_Game.Components)
            {
                screensManager = component as ScreensManager;
                if (screensManager != null)
                {
                    break;
                }
            }
            return screensManager;
        }

        //public static List<Text> GetTextInstructions(GameScreen i_GameScreen, Color i_Color, float i_Scale, params string[] i_Instructions)
        //{
        //    List<Text> textInstructions = new List<Text>();
        //    Text text;
        //    foreach(string instruction in i_Instructions)
        //    {
        //        text = SpritesFactory.CreateSprite(i_GameScreen, SpritesFactory.eSpriteType.SmallText) as Text;
        //        text.TintColor = i_Color;
        //        text.Scales = new Vector2(i_Scale);
        //    }
        //    return textInstructions;
        //}

        public static void ClearComponents<T>(GameScreen i_GameScreen)
            where T : class
        {
            List<GameComponent> compsToBeDeleted = new List<GameComponent>();
            T comp ;
            foreach (GameComponent component in i_GameScreen)
            {
                comp = component as T;
                if (comp != null)
                {
                    compsToBeDeleted.Add(component as GameComponent);

                }
            }
            foreach(GameComponent item in compsToBeDeleted)
            {
                i_GameScreen.Remove(item);
            }
        }
    }
}
