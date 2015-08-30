using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
namespace SpaceInvaders.Infrastructure.ObjectModel.Sound
{
    public class Sound 
    {
        private Game m_Game;
        private string m_AssetName;
        private SoundEffectInstance m_SoundEffectInstance;

        public Sound(GameScreen i_GameScreen, string i_AssetName)
        {
            m_Game = i_GameScreen.Game;
            m_AssetName = i_AssetName;
            m_SoundEffectInstance = m_Game.Content.Load<SoundEffect>(i_AssetName).CreateInstance();
            m_SoundEffectInstance.IsLooped = false;
            Volume = 1f;
            Pitch = 0;
            Pan = 0;

        }

        public Sound(Game i_Game, string i_AssetName)
        {
            m_Game = i_Game;
            m_AssetName = i_AssetName;
            m_SoundEffectInstance =  m_Game.Content.Load<SoundEffect>(i_AssetName).CreateInstance();
            m_SoundEffectInstance.IsLooped = false;
            Volume = 1f;
            Pitch = 0;
            Pan = 0;

        }
        public void Play()
        {
            m_SoundEffectInstance.Play();
        }
        public bool isLooped
        {
            get
            {
                return m_SoundEffectInstance.IsLooped;
            }
            set
            {
                m_SoundEffectInstance.IsLooped = value;
            }
        }
        public void Resume()
        {
            m_SoundEffectInstance.Resume();
        }
        public void Stop()
        {
            m_SoundEffectInstance.Stop();
        }
        public float Volume
        {
            get
            {
                return m_SoundEffectInstance.Volume;
            }
            set
            {
                if (value >= 0 && value <= 1)
                {
                    m_SoundEffectInstance.Volume = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Out of volume range ,Expected 0 to 1");
                }
            }
        }
        public float Pitch
        {
            get
            {
                return m_SoundEffectInstance.Pitch;
            }
            set
            {
                if (value > -1 && value <= 1)
                {
                    m_SoundEffectInstance.Pitch = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Out of pitch range ,Expected 0 to 1");
                }
            }
        }
        public void PlayInLoop()
        {
            this.isLooped = true;
            Play();
        }
        public float Pan
        {
            get
            {
                return m_SoundEffectInstance.Pan;
            }
            set
            {
                if (value > -1 && value <= 1)
                {
                    m_SoundEffectInstance.Pan = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Out of Pan range ,Expected 0 to 1");
                }
            }
        }
    }
}
