﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.ObjectModel.Sound
{
  public  class VolumeConfiguration
    {
      float m_Volume;
      float m_MinValue;
      float m_MaxValue;
      float m_JumpingScale;
      public VolumeConfiguration(float i_SoundStartValue,float i_ValidMinValue,float i_ValidMaxValue,float i_JumpingScale)
      {
          m_Volume = i_SoundStartValue;
          m_MinValue=i_ValidMinValue;
          m_MaxValue=i_ValidMaxValue;
          m_JumpingScale = i_JumpingScale;
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
          }
      public void Decrease()
      {
          if (validValue(m_Volume - m_JumpingScale))
          {
              m_Volume -= m_JumpingScale;
          }
      }
    }
}
