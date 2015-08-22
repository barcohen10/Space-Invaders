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

namespace SpaceInvaders.Services
{
    public static class SpaceInvadersServices
    {
        public static void CreateNewPlayers(Game i_Game, params ConfSpaceShip[] i_ConfSpaceShips)
        {
            PlayerSpaceInvaders player = null, beforePlayer = null;
            float xSpaceShip, yLifes = 0, xLifes = (float)i_Game.GraphicsDevice.Viewport.Width - 20;
            for (int i = 0; i < i_ConfSpaceShips.Length; i++)
            {
                string nickname = "P" + (i + 1).ToString();
                player = new PlayerSpaceInvaders(i_Game, nickname, i_ConfSpaceShips[i].SpaceShipType);
                foreach (Life life in player.LifesSprites)
                {
                    life.Position = new Vector2(xLifes, yLifes);
                    xLifes -= 20;
                }

                yLifes += 20;
                xLifes = (float)i_Game.GraphicsDevice.Viewport.Width - 20;
                player.SpaceShip.Configuration = i_ConfSpaceShips[i];
                if (beforePlayer != null)
                {
                    xSpaceShip = beforePlayer.SpaceShip.Position.X + beforePlayer.SpaceShip.Width + (beforePlayer.SpaceShip.Width / 2);
                    player.SpaceShip.Position = new Vector2(xSpaceShip, 0);
                    player.ScoreMessage.Position = new Vector2(beforePlayer.ScoreMessage.Position.X, beforePlayer.ScoreMessage.Position.Y + 20);
                }

                beforePlayer = player;
                i_Game.Components.Add(player);
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

        public static int GetShootingSpriteAmountOfAliveBullets(Game i_Game, ShootingSprite i_ShottingSprite)
        {
            int aliveBullets = 0;
            string typeName;
            foreach (GameComponent component in i_Game.Components)
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

        public static EnemiesMatrix GetEnemeiesMatrixComponent(Game i_Game)
        {
            EnemiesMatrix enemiesMatrix = null;
            foreach (GameComponent component in i_Game.Components)
            {
                enemiesMatrix = component as EnemiesMatrix;
                if (enemiesMatrix != null)
                {
                    break;
                }
            }

            return enemiesMatrix;
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

        public static void ChangeBarriersGroupVerticalPosition(Game i_Game, BarrierGroup i_BarrierGroup)
        {
            SpaceShip spaceShip = null;
            List<PlayerSpaceInvaders> allPlayers = getAllPlayers(i_Game);
            float y;
            if (allPlayers.Count > 0)
            {
                spaceShip = allPlayers[0].SpaceShip;
                y = spaceShip.Position.Y - (spaceShip.Width * 2);
                i_BarrierGroup.ChangeGroupPositionY(y);
            }
        }
    }
}
