using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class CellAnimator : SpriteAnimator
    {
        private TimeSpan m_CellTime;
        private TimeSpan m_TimeLeftForCell;
        private bool m_Loop = true;
        private int m_CurrCellIdx = 0;
        private readonly int r_NumOfCells = 1;
        private readonly int r_StartCellIndex = 0;

        public TimeSpan CellTime { get { return m_CellTime; } set { m_CellTime = value; } }

        // CTORs
        public CellAnimator(TimeSpan i_CellTime, int i_StartCellIndex,int i_NumOfCells, TimeSpan i_AnimationLength)
            : base("CellAnimator", i_AnimationLength)
        {
            this.m_CellTime = i_CellTime;
            this.m_TimeLeftForCell = i_CellTime;
            this.r_NumOfCells = i_NumOfCells;
            this.r_StartCellIndex = i_StartCellIndex;
            this.m_CurrCellIdx = i_StartCellIndex;
            m_Loop = i_AnimationLength == TimeSpan.Zero;
        }

        private void goToNextFrame()
        {
            m_CurrCellIdx++;
            if (m_CurrCellIdx >= r_NumOfCells)
            {
                if (m_Loop)
                {
                    m_CurrCellIdx = r_StartCellIndex;
                }
                else
                {
                    m_CurrCellIdx = r_NumOfCells - 1; /// lets stop at the last frame
                    this.IsFinished = true;
                }
            }
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.SourceRectangle = m_OriginalSpriteInfo.SourceRectangle;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            if (m_CellTime != TimeSpan.Zero)
            {
                m_TimeLeftForCell -= i_GameTime.ElapsedGameTime;
                if (m_TimeLeftForCell.TotalSeconds <= 0)
                {
                    /// we have elapsed, so blink
                    goToNextFrame();
                    m_TimeLeftForCell = m_CellTime;
                }
            }

            this.BoundSprite.SourceRectangle = new Rectangle(
                m_CurrCellIdx * this.BoundSprite.SourceRectangle.Width,
                this.BoundSprite.SourceRectangle.Top,
                this.BoundSprite.SourceRectangle.Width,
                this.BoundSprite.SourceRectangle.Height);
        }

        public void ChangeShape()
        {
            goToNextFrame();
            m_TimeLeftForCell = m_CellTime;
        }
    }
}
