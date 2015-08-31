using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Services;
using SpaceInvaders.Services;

namespace SpaceInvaders.Screens
{
    public class PauseScreen : GameScreen
    {
        private Text m_MessageSprite;

        public PauseScreen(Game i_Game)

            : base(i_Game)
        {
            m_MessageSprite = SpritesFactory.CreateSprite(this, SpritesFactory.eSpriteType.MediumText) as Text;
            m_MessageSprite.Position = new Vector2(180, 230);
            (m_MessageSprite as Text).TextString = "Click R to continue playing";
            TextServices.CenterTextsOnScreen(this, new List<Text>() { (m_MessageSprite as Text) });
            this.IsModal = true;
            this.IsOverlayed = true;
            this.UseGradientBackground = true;
            this.BlackTintAlpha = 0.4f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Microsoft.Xna.Framework.Input.Keys.R))
            {
                this.ExitScreen();
            }
        }
    }
}
