using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.ObjectModel.Sound
{
    public class VolumeInstance
    {
        float m_Volume;
        float m_MinValue;
        float m_MaxValue;
        float m_JumpingScale;
        private Type m_SoundType;
      public Type SoundType { get { return m_SoundType; } }
        public event EventHandler<EventArgs> VolumeChange;

        protected virtual void OnVolumeChange()
        {
            if (VolumeChange != null)
            {
                VolumeChange(this, EventArgs.Empty);
            }
        }
        public VolumeInstance(float i_SoundStartValue, float i_ValidMinValue, float i_ValidMaxValue, float i_JumpingScale,Type i_SoundType)
        {
            m_Volume = i_SoundStartValue;
            m_MinValue = i_ValidMinValue;
            m_MaxValue = i_ValidMaxValue;
            m_JumpingScale = i_JumpingScale;
            m_SoundType = i_SoundType;
        }
        
        public float Volume { get { return m_Volume; } }
        private bool validValue(float i_value)
        {
            return (i_value >= m_MinValue && i_value <= m_MaxValue);
        }
        public void Increase()
        {
            if (validValue(m_Volume + m_JumpingScale))
            {
                m_Volume += m_JumpingScale;
            }
            OnVolumeChange();
        }
        public void Decrease()
        {
            if (validValue(m_Volume - m_JumpingScale))
            {
                m_Volume -= m_JumpingScale;
            }
            OnVolumeChange();
        }

    }
}
