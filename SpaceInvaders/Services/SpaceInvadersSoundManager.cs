using C15Ex03Dotan301810610Bar308000322.ServiceInterfaces;
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
    public class SpaceInvadersSoundManager : SoundsManager, ISpaceInvadersSoundsManager
    {
        public void Initialize()
        {
            this.AddVolumeInstance("Background", new VolumeInstance(0.5f, 0, 1f, 0.1f, typeof(BackgroundSound)));
            this.AddVolumeInstance("SoundEffect", new VolumeInstance(0.5f, 0, 1f, 0.1f, typeof(SoundEffect)));
        }

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
