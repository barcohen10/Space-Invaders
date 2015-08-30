using C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuScreens;
using C15Ex03Dotan301810610Bar308000322.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Screens
{
    public class GameOverScreen : TitleScreen
    {
        private List<string> m_PlayersScore;
        public GameOverScreen(Game i_Game, List<string> i_PlayersScore)
            : base(i_Game, "Game Over", Color.Red, "[P] - Start new game", "[F6] - Main menu", "[Esc] - Exit game")
        {
            m_PlayersScore = i_PlayersScore;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (InputManager.KeyPressed(Keys.P))
            {
                ScreensManager.SetCurrentScreen(new GamingScreen(this.Game));
                ScreensManager.SetCurrentScreen(new MoveStageScreen(this.Game, 1));
             }
            else if (InputManager.KeyPressed(Keys.Escape))
            {
                this.Game.Exit();
            }
            else if (InputManager.KeyPressed(Keys.F6))
            {
                ScreensManager.SetCurrentScreen(new MainMenuScreen(this.Game));
            }
        }

        protected override void initTexts()
        {
            base.initTexts();
            Vector2 scorePosition = new Vector2(30, m_InstructionsText[m_InstructionsText.Count - 1].Position.Y + m_InstructionsText[m_InstructionsText.Count - 1].Height * 1.5f);
            Text playerScoreText;
            foreach(string playerScore in m_PlayersScore)
            {
                playerScoreText = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.BigText) as Text;
                playerScoreText.TintColor = Color.Red;
                playerScoreText.Position = scorePosition;
                playerScoreText.TextString = playerScore;
                scorePosition = new Vector2(scorePosition.X, scorePosition.Y + playerScoreText.Height * 1.3f);
            }

        }
    }
}
