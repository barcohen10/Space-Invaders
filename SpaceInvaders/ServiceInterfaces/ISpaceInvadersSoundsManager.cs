using System;
namespace C15Ex03Dotan301810610Bar308000322.ServiceInterfaces
{
   public interface ISpaceInvadersSoundsManager
    {
        void DecreaseBackgroundMusic();
        void DecreaseSoundEffect();
        void IncreaseBackgroundMusic();
        void IncreaseSoundEffect();
        void Mute();
        void Play();
    }
}
