using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.Managers;
using SpaceInvaders.Infrastructure.ObjectModel.Sound;
using SpaceInvaders.Infrastructure.ObjectModel.Sound.ConcreteSounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Services
{
    public class SpaceInvadersSoundsManager : SoundsManager
    {
        private VolumeInstance m_BackgroundSound, m_SoundEffect;

        public SpaceInvadersSoundsManager()
        {
            m_BackgroundSound = new VolumeInstance(0.5f, 0, 1f, 0.1f, typeof(BackgroundSound));
            m_SoundEffect = new VolumeInstance(0.5f, 0, 1f, 0.1f, typeof(SoundEffect));
            this.AddVolumeInstance("Background", m_BackgroundSound);
            this.AddVolumeInstance("SoundEffect", m_SoundEffect);
        }

        public VolumeInstance BackgroundSound { get { return m_BackgroundSound; } set { m_BackgroundSound = value; } }

        public VolumeInstance SoundEffect { get { return m_SoundEffect; } set { m_SoundEffect = value; } }

        public void DecreaseBackgroundMusic()
        {
            this["Background"].Decrease();
        }

        public void DecreaseSoundEffect()
        {
            this["SoundEffect"].Decrease();
        }

        public void IncreaseBackgroundMusic()
        {
            this["Background"].Increase();
        }

        public void IncreaseSoundEffect()
        {
            this["SoundEffect"].Increase();

        }

    }
}
