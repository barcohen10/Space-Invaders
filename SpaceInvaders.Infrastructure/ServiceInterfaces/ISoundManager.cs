using System;
namespace SpaceInvaders.Infrastructure.ServiceInterfaces
{
   public interface ISoundManager
    {
        void DecreaseBackgroundMusic();
        void DecreaseSoundEffect();
        void IncreaseBackgroundMusic();
        void IncreaseSoundEffect();
        void Mute();
        void Play();
    }
}
