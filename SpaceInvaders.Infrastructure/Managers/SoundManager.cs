using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel.Sound;
using SpaceInvaders.Infrastructure.ObjectModel.Sound.ConcreteSounds;
using SpaceInvaders.Infrastructure.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.Managers
{
    public class SoundsManager : GameComponent, ISoundsManager
    {
        private bool m_SoundOn = true;
        private VolumeInstance m_BackgroundVolume = new VolumeInstance(0.5f, 0, 1, 0.1f);
        private VolumeInstance m_SoundEffectVolume = new VolumeInstance(0.5f, 0, 1, 0.1f);
        private List<Sound> m_Sounds = new List<Sound>();
        public SoundsManager(Game i_Game)
            : base(i_Game)
        {
            i_Game.Components.Add(this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (Sound sound in m_Sounds)
            {
                if (m_SoundOn)
                {
                    if (sound.GetType() == typeof(BackgroundSound))
                    {
                        sound.Volume = m_BackgroundVolume.Volume;
                    }
                    else
                    {
                        sound.Volume = m_SoundEffectVolume.Volume;
                    }
                }
            }
        }
        public void Mute()
        {
            foreach (Sound sound in m_Sounds)
            {
                sound.Volume = 0;
            }
            m_SoundOn = false;
        }
        public void Play()
        {
            m_SoundOn = true;
        }
        public void Add(Sound i_Sound)
        {
            m_Sounds.Add(i_Sound);
        }
        public void Remove(Sound i_Sound)
        {
            m_Sounds.Remove(i_Sound);
        }
        public void IncreaseBackgroundMusic()
        {
            m_BackgroundVolume.Increase();
        }
        public void IncreaseSoundEffect()
        {
            m_SoundEffectVolume.Increase();
        }
        public void DecreaseBackgroundMusic()
        {
            m_BackgroundVolume.Increase();
        }
        public void DecreaseSoundEffect()
        {
            m_SoundEffectVolume.Increase();
        }
    }
}
