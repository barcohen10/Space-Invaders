using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.Services
{
    public static class SpritesFactory
    {
        public enum eSpriteType
        {
            Bullet,
            EnemyPink,
            EnemyYellow,
            EnemyLightBlue,
            BlueSpaceShip,
            GreenSpaceShip,
            SpaceBackground,
            MotherShip,
            Barrier,
            Text,
            LifeBlueSpaceShip,
            LifeGreenSpaceShip
        }

        private const string k_EnemysAsset = @"Sprites\Enemies";
        private const string k_MotherSpaceShipAsset = @"Sprites\MotherSpaceShip";
        private const string k_GreenSpaceShipAsset = @"Sprites\SpaceShip2";
        private const string k_BlueSpaceShipAsset = @"Sprites\SpaceShip";
        private const string k_SpaceBGAsset = @"Sprites\SpaceBg";
        private const string k_BulletAsset = @"Sprites\Bullet";
        private const string k_BarrierAsset = @"Sprites\Barrier";
        private const string k_CalibriFontAsset = @"Fonts\CalibriFont";

        public static Sprite CreateSprite(GameScreen i_GameScreen, eSpriteType i_SpriteType)
        {
            Sprite sprite = null;
            int pointsEarnedWhenKilled = 0;
            switch (i_SpriteType)
            {
                case eSpriteType.Bullet:
                    sprite = new Bullet(i_GameScreen, k_BulletAsset);
                    break;
                case eSpriteType.EnemyLightBlue:
                    pointsEarnedWhenKilled = int.Parse(ConfigurationManager.AppSettings["Scores.LightBlueEnemy"].ToString());
                    sprite = new Enemy(i_GameScreen, Color.LightBlue, 2, 4, k_EnemysAsset) { Points = pointsEarnedWhenKilled };
                    break;
                case eSpriteType.EnemyPink:
                    pointsEarnedWhenKilled = int.Parse(ConfigurationManager.AppSettings["Scores.PinkEnemy"].ToString());
                    sprite = new Enemy(i_GameScreen, Color.Pink, 0, 2, k_EnemysAsset) { Points = pointsEarnedWhenKilled };
                    break;

                case eSpriteType.EnemyYellow:
                    pointsEarnedWhenKilled = int.Parse(ConfigurationManager.AppSettings["Scores.YellowEnemy"].ToString());
                    sprite = new Enemy(i_GameScreen, Color.Yellow, 4, 6, k_EnemysAsset) { Points = pointsEarnedWhenKilled };
                    break;

                case eSpriteType.MotherShip:
                    pointsEarnedWhenKilled = int.Parse(ConfigurationManager.AppSettings["Scores.Mothership"].ToString());
                    sprite = new MotherShip(i_GameScreen, k_MotherSpaceShipAsset) { Points = pointsEarnedWhenKilled };

                    break;

                case eSpriteType.SpaceBackground:
                    sprite = new SpaceBackground(i_GameScreen, k_SpaceBGAsset);
                    break;

                case eSpriteType.BlueSpaceShip:
                    sprite = new SpaceShip(i_GameScreen, k_BlueSpaceShipAsset);
                    break;

                case eSpriteType.GreenSpaceShip:
                    sprite = new SpaceShip(i_GameScreen, k_GreenSpaceShipAsset);
                    break;

                case eSpriteType.Barrier:
                    sprite = new Barrier(i_GameScreen, k_BarrierAsset);
                    break;

                case eSpriteType.Text:
                    sprite = new Text(i_GameScreen, k_CalibriFontAsset);
                    break;

                case eSpriteType.LifeBlueSpaceShip:
                    sprite = new Life(i_GameScreen, k_BlueSpaceShipAsset);
                    break;

                case eSpriteType.LifeGreenSpaceShip:
                    sprite = new Life(i_GameScreen, k_GreenSpaceShipAsset);
                    break;
            }
            sprite.Initialize();
            i_GameScreen.Add(sprite);
            return sprite;
        }
    }
}
