using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.ObjectModel.Sound
{
    public class VolumeInstance
    {
        private Type m_SoundType;
        private float m_Volume, m_MinValue, m_MaxValue,m_VolumeBeforeMute, m_JumpingScale;
        public Type SoundType { get { return m_SoundType; } }
        public event EventHandler<EventArgs> VolumeChange;
        private bool m_Muted = false;

        protected virtual void OnVolumeChange()
        {
            if (VolumeChange != null)
            {
                VolumeChange(this, EventArgs.Empty);
            }
        }

        public VolumeInstance(float i_SoundStartValue, float i_ValidMinValue, float i_ValidMaxValue, float i_JumpingScale, Type i_SoundType)
        {
            m_Volume = i_SoundStartValue;
            m_MinValue = i_ValidMinValue;
            m_MaxValue = i_ValidMaxValue;
            m_VolumeBeforeMute = i_SoundStartValue;
            m_JumpingScale = i_JumpingScale;
            m_SoundType = i_SoundType;
        }

        public float Volume { get { return m_Volume; } }

        public void Increase()
        {

            m_Volume = MathHelper.Clamp(m_Volume + m_JumpingScale, m_MinValue, m_MaxValue);
            if (!m_Muted)
            {
                OnVolumeChange();
            }
        }

        public void Decrease()
        {
            m_Volume = MathHelper.Clamp(m_Volume - m_JumpingScale, m_MinValue, m_MaxValue);
            if (!m_Muted)
            {
                OnVolumeChange();
            }
        }

        public void Mute()
        {
            m_Muted = true;
            m_Volume = 0;
            OnVolumeChange();
        }

        public void UnMute()
        {
            m_Muted = false;
            m_Volume = m_VolumeBeforeMute;
            OnVolumeChange();

        }


    }
}
