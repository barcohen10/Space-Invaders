using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex02Dotan301810610Bar308000322.Services
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
        private const string k_BackgroundMusicAsset = @"Sounds\BackgroundMusic";
        private const string k_EnemyGunShotAsset = @"Sounds\EnemyGunShot";
        private const string k_EnemyKillAsset = @"Sounds\EnemyKill";
        private const string k_GameOverAsset = @"Sounds\GameOver";
        private const string k_LevelWinAsset = @"Sounds\LevelWin";
        private const string k_LifeDieAsset = @"Sounds\LifeDie";
        private const string k_MenuMoveAsset = @"Sounds\MenuMove";
        private const string k_MotherShipKillAsset = @"Sounds\MotherShipKill";
        private const string k_SSGunShotAsset = @"Sounds\SSGunShot";

        public static Sound CreateSound(GameScreen i_GameScreen, eSoundType i_eSoundType)
        {

            Sound sound = null;
            switch (i_eSoundType)
            {
                case eSoundType.BackgroundMusic:
                    sound = new Sound(i_GameScreen, k_BackgroundMusicAsset);
                    break;
                case eSoundType.BarrierHit:
                    sound = new Sound(i_GameScreen, k_BarrierHitAsset);
                    break;
                case eSoundType.EnemyGunShot:
                    sound = new Sound(i_GameScreen, k_EnemyGunShotAsset);
                    break;
                case eSoundType.EnemyKill:
                    sound = new Sound(i_GameScreen, k_EnemyKillAsset);
                    break;
                case eSoundType.GameOver:
                    sound = new Sound(i_GameScreen, k_GameOverAsset);
                    break;
                case eSoundType.LevelWin:
                    sound = new Sound(i_GameScreen, k_LevelWinAsset);
                    break;
                case eSoundType.LifeDie:
                    sound = new Sound(i_GameScreen, k_LifeDieAsset);
                    break;
                case eSoundType.MenuMove:
                    sound = new Sound(i_GameScreen, k_MenuMoveAsset);
                    break;
                case eSoundType.MotherShipKill:
                    sound = new Sound(i_GameScreen, k_MotherShipKillAsset);
                    break;
                case eSoundType.SSGunShot:
                    sound = new Sound(i_GameScreen, k_SSGunShotAsset);
                    break;
            }
            return sound;
        }
    }
}
