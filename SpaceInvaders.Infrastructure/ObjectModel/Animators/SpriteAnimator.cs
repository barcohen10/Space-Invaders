//*** Guy Ronen © 2008-2011 ***//
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModels;

namespace Infrastructure.ObjectModel.Animators
{
    public abstract class SpriteAnimator
    {
        private             Sprite m_BoundSprite;
        private TimeSpan    m_AnimationLength;
        private TimeSpan    m_TimeLeft;
        private bool        m_IsFinished = false;
        private bool        m_Enabled = true;
        private bool        m_Initialized = false;
        private string      m_Name;
        protected bool      m_ResetAfterFinish = true;
        protected internal  Sprite m_OriginalSpriteInfo;

        public event EventHandler Finished;

        protected virtual void OnFinished()
        {
            if (m_ResetAfterFinish)
            {
                Reset();
                this.m_IsFinished = true;
            }

            if (Finished != null)
            {
                Finished(this, EventArgs.Empty);
            }
        }

        protected SpriteAnimator(string i_Name, TimeSpan i_AnimationLength)
        {
            m_Name = i_Name;
            m_AnimationLength = i_AnimationLength;
        }

        protected internal Sprite BoundSprite
        {
            get { return m_BoundSprite; }
            set { m_BoundSprite = value; }
        }

        public string Name
        {
            get { return m_Name; }
        }

        public bool Enabled
        {
            get { return m_Enabled; }
            set { m_Enabled = value; }
        }

        public bool IsFinite
        {
            get { return this.m_AnimationLength != TimeSpan.Zero; }
        }

        public bool ResetAfterFinish
        {
            get { return m_ResetAfterFinish; }
            set { m_ResetAfterFinish = value; }
        }

        public virtual void Initialize()
        {
            if (!m_Initialized)
            {
                m_Initialized = true;

                CloneSpriteInfo();

                Reset();
            }
        }

        protected virtual void CloneSpriteInfo()
        {
            if (m_OriginalSpriteInfo == null)
            {
                m_OriginalSpriteInfo = m_BoundSprite.ShallowClone();
            }
        }

        public void Reset()
        {
            Reset(m_AnimationLength);
        }

        public void Reset(TimeSpan i_AnimationLength)
        {
            if (!m_Initialized)
            {
                Initialize();
            }
            else
            {
                m_AnimationLength = i_AnimationLength;
                m_TimeLeft = m_AnimationLength;
                this.IsFinished = false;
            }

            RevertToOriginal();
        }

        protected abstract void RevertToOriginal();

        public void Pause()
        {
            this.Enabled = false;
        }

        public void Resume()
        {
            m_Enabled = true;
        }

        public virtual void Restart()
        {
            Restart(m_AnimationLength);
        }

        public virtual void Restart(TimeSpan i_AnimationLength)
        {
            Reset(i_AnimationLength);
            Resume();
        }

        protected TimeSpan AnimationLength
        {
            get { return m_AnimationLength; }
        }

        public bool IsFinished
        {
            get { return this.m_IsFinished; }
            protected set
            {
                if (value != m_IsFinished)
                {
                    m_IsFinished = value;
                    if (m_IsFinished == true)
                    {
                        OnFinished();
                    }
                }
            }
        }

        public void Update(GameTime i_GameTime)
        {
            if (!m_Initialized)
            {
                Initialize();
            }

            if (this.Enabled && !this.IsFinished)
            {
                if (this.IsFinite)
                {
                    // check if we should stop animating:
                    m_TimeLeft -= i_GameTime.ElapsedGameTime;

                    if (m_TimeLeft.TotalSeconds < 0)
                    {
                        this.IsFinished = true;
                    }
                }

                if (!this.IsFinished)
                {
                    // we are still required to animate:
                     DoFrame(i_GameTime);
                }
            }
        }

        protected abstract void DoFrame(GameTime i_GameTime);
    }
}
