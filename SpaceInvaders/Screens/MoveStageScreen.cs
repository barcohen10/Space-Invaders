using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex02Dotan301810610Bar308000322.Screens
{
    public class MoveStageScreen : GameScreenWithTimer
    {

        public MoveStageScreen(Game i_Game)
            : base(i_Game,TimeSpan.FromSeconds(3))
        {
            Finished += MoveStageScreen_Finished;
        }

        void MoveStageScreen_Finished(object sender, EventArgs e)
        {
            this.ExitScreen();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }
    }
}
