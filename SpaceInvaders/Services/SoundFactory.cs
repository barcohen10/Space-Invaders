using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.Managers;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModel.Sound;
using SpaceInvaders.Infrastructure.ObjectModel.Sound.ConcreteSounds;
using SpaceInvaders.Services;

namespace SpaceInvaders.Services
{
    public class SoundFactory
    {
        public enum eSoundType
        {
            BarrierHit,
            BackgroundMusic,
            EnemyGunShot,
            EnemyKill,
            GameOver,
            LevelWin,
            LifeDie,
            MenuMove,
            MotherShipKill,
            SSGunShot,
        }

        private const string k_BarrierHitAsset = @"Sounds\BarrierHit";
        private const string k_BackgroundMusicAsset = @"Sounds\BGMusic";
        private const string k_EnemyGunShotAsset = @"Sounds\EnemyGunShot";
        private const string k_EnemyKillAsset = @"Sounds\EnemyKill";
        private const string k_GameOverAsset = @"Sounds\GameOver";
        private const string k_LevelWinAsset = @"Sounds\LevelWin";
        private const string k_LifeDieAsset = @"Sounds\LifeDie";
        private const string k_MenuMoveAsset = @"Sounds\MenuMove";
        private const string k_MotherShipKillAsset = @"Sounds\MotherShipKill";
        private const string k_SSGunShotAsset = @"Sounds\SSGunShot";

        public static Sound CreateSound(Game i_Game, eSoundType i_eSoundType)
        {
            SoundsManager m_soundManager = SpaceInvadersServices.GetSoundManager(i_Game);
            Sound sound = null;
            switch (i_eSoundType)
            {
                case eSoundType.BackgroundMusic:
                    sound = new BackgroundSound(i_Game, k_BackgroundMusicAsset);
                    break;
                case eSoundType.BarrierHit:
                    sound = new SoundEffect(i_Game, k_BarrierHitAsset);
                    break;
                case eSoundType.EnemyGunShot:
                    sound = new SoundEffect(i_Game, k_EnemyGunShotAsset);
                    break;
                case eSoundType.EnemyKill:
                    sound = new SoundEffect(i_Game, k_EnemyKillAsset);
                    break;
                case eSoundType.GameOver:
                    sound = new SoundEffect(i_Game, k_GameOverAsset);
                    break;
                case eSoundType.LevelWin:
                    sound = new SoundEffect(i_Game, k_LevelWinAsset);
                    break;
                case eSoundType.LifeDie:
                    sound = new SoundEffect(i_Game, k_LifeDieAsset);
                    break;
                case eSoundType.MenuMove:
                    sound = new SoundEffect(i_Game, k_MenuMoveAsset);
                    break;
                case eSoundType.MotherShipKill:
                    sound = new SoundEffect(i_Game, k_MotherShipKillAsset);
                    break;
                case eSoundType.SSGunShot:
                    sound = new SoundEffect(i_Game, k_SSGunShotAsset);
                    break;
            }

            m_soundManager.SetSoundByInstanceType(sound);
            m_soundManager.AddSound(sound);
            return sound;
        }

        public static Sound CreateSound(GameScreen i_GameScreen, eSoundType i_SoundType)
        {
            return CreateSound(i_GameScreen.Game, i_SoundType);
        }
    }
}
