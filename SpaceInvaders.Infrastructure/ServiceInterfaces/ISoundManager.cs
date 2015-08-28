using System;
namespace SpaceInvaders.Infrastructure.ServiceInterfaces
{
    interface ISoundManager
    {
        void DecraseBackgroundMusic();
        void DecraseSoundEffect();
        void IncraseBackgroundMusic();
        void IncraseSoundEffect();
        void Mute();
        void Play();
    }
}
