using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
namespace SpaceInvaders.Infrastructure.ObjectModel
{
    public class Sound
    {
        private Game m_Game;
        private GameScreen m_GameScreen;
        private string m_AssetName;
        private SoundEffect m_SoundEffect;
        private SoundEffectInstance m_SoundEffectInstance;

        public Sound(GameScreen i_GameScreen, string i_AssetName)
        {
            m_GameScreen = i_GameScreen;
            m_Game = i_GameScreen.Game;
            m_AssetName = i_AssetName;
            m_SoundEffect = m_Game.Content.Load<SoundEffect>(i_AssetName);
            m_SoundEffectInstance = m_SoundEffect.CreateInstance();
            m_SoundEffectInstance.IsLooped = false;
        }
        public void Play()
        {
            m_SoundEffectInstance.Play();

        }
        public bool isLooped
        {
            get
            {
                return m_SoundEffectInstance.IsLooped; ;
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
                if (value > 0 && value <= 1)
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
    }
}
