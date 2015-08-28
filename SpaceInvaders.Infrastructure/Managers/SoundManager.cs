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
    public class SoundManager : GameComponent,ISoundManager
    {
        private float m_soundEffectVolume = 0.5f;
        private float m_BackgroundVolume = 0.5f;
        private bool m_SoundOn = true;

        private List<Sound> m_Sounds = new List<Sound>();
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (Sound sound in m_Sounds)
            {
                if (m_SoundOn)
                {
                    if (sound.GetType() == typeof(BackgroundSound))
                    {
                        sound.Volume = m_BackgroundVolume;
                    }
                    else
                    {
                        sound.Volume = m_soundEffectVolume;
                    }
                }
            }
        }
        public SoundManager(Game i_Game)
            : base(i_Game)
        {
            i_Game.Components.Add(this);
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
        public void IncraseBackgroundMusic()
        {
            m_BackgroundVolume += 0.1f;
        }
        public void DecraseBackgroundMusic()
        {
            m_BackgroundVolume -= 0.1f;
        }
        public void IncraseSoundEffect()
        {
            m_soundEffectVolume += 0.1f;
        }
        public void DecraseSoundEffect()
        {
            m_soundEffectVolume -= 0.1f;
        }
        public void Add(Sound i_Sound)
        {
            m_Sounds.Add(i_Sound);
        }
        public void Remove(Sound i_Sound)
        {
            m_Sounds.Remove(i_Sound);
        }
    }
}
