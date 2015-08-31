using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel.Sound;
using SpaceInvaders.Infrastructure.ObjectModel.Sound.ConcreteSounds;
using SpaceInvaders.Infrastructure.ServiceInterfaces;

namespace SpaceInvaders.Infrastructure.Managers
{
    public class SoundsManager
    {
        private Dictionary<string, VolumeInstance> m_VolumeInstances;
        private List<Sound> m_Sounds = new List<Sound>();
        private bool m_Muted = false;

        public SoundsManager()
        {
            m_VolumeInstances = new Dictionary<string, VolumeInstance>();
            m_Sounds = new List<Sound>();
        }

        private bool isMuted()
        {
            return m_Muted;
        }

        public string SoundStatus
        {
            get
            {
                string result = "Off";
                if (!isMuted())
                {
                    result = "On";
                }

                return result;
            }
        }

        private void updateSoundList(object sender, EventArgs e)
        {
            foreach (Sound sound in m_Sounds)
            {
                foreach (KeyValuePair<string, VolumeInstance> volumeInstance in m_VolumeInstances)
                {
                    if (volumeInstance.Value.SoundType == sound.GetType())
                    {
                        sound.Volume = volumeInstance.Value.Volume;
                    }
                }
            }
        }

        public void Mute()
        {
            m_Muted = true;
            foreach (KeyValuePair<string, VolumeInstance> volumeInstance in m_VolumeInstances)
            {
                volumeInstance.Value.Mute();
            }
        }

        public void AddSound(Sound i_Sound)
        {
            m_Sounds.Add(i_Sound);
        }

        public void RemoveSound(Sound i_Sound)
        {
            m_Sounds.Remove(i_Sound);
        }

        public void AddVolumeInstance(string i_InstanceName, VolumeInstance i_VolumeInstance)
        {
            i_VolumeInstance.VolumeChange += updateSoundList;
            m_VolumeInstances.Add(i_InstanceName, i_VolumeInstance);
        }

        public void RemoveVolumeInstance(string i_InstanceName)
        {
            m_VolumeInstances.Remove(i_InstanceName);
        }

        public VolumeInstance this[string i_InstanceName]
        {
            get
            {
                VolumeInstance instance = null;
                m_VolumeInstances.TryGetValue(i_InstanceName, out instance);
                return instance;
            }
        }

        public void Play()
        {
            m_Muted = false;
            foreach (KeyValuePair<string, VolumeInstance> volumeInstance in m_VolumeInstances)
            {
                volumeInstance.Value.UnMute();
            }
        }

        public void SetSoundByInstanceType(Sound i_Sound)
        {
            foreach (KeyValuePair<string, VolumeInstance> volumeInstance in this.m_VolumeInstances)
            {
                if (volumeInstance.Value.SoundType == i_Sound.GetType())
                {
                    i_Sound.Volume = volumeInstance.Value.Volume;
                    break;
                }
            }
        }
    }
}
