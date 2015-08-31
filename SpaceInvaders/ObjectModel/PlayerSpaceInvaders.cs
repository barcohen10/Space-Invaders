using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.ObjectModel;
using SpaceInvaders.Services;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ObjectModel.Sound;

namespace SpaceInvaders.ObjectModel
{
    public class PlayerSpaceInvaders : Player
    {
        private SpaceShip m_SpaceShip;
        private Text m_ScoreText;
        private List<Life> m_LifesSprites = new List<Life>();
        private string m_Nickname;
        private eSpaceShipType m_SpaceShipType;

        public enum eSpaceShipType
        {
            Green,
            Blue
        }

        public List<Life> LifesSprites
        {
            get { return m_LifesSprites; }
            set { m_LifesSprites = value; }
        }

        public SpaceShip SpaceShip
        {
            get
            {
                return m_SpaceShip;
            }

            set
            {
                m_SpaceShip = value;
            }
        }

        public Text ScoreMessage
        {
            get
            {
                return m_ScoreText;
            }

            set
            {
                m_ScoreText = value;
            }
        }

        public PlayerSpaceInvaders(GameScreen i_GameScreen, string i_PlayerNickname, eSpaceShipType i_SpaceShipType)
            : base(i_GameScreen.Game, i_PlayerNickname)
        {
            m_LoseLifeSound = SoundFactory.CreateSound(this.Game, SoundFactory.eSoundType.LifeDie) as Sound;
            m_Nickname = i_PlayerNickname;
            m_SpaceShipType = i_SpaceShipType;
            m_ScoreText = SpritesFactory.CreateSprite(i_GameScreen, SpritesFactory.eSpriteType.SmallText) as Text;
            SpritesFactory.eSpriteType lifeType = SpritesFactory.eSpriteType.LifeBlueSpaceShip;

            switch (m_SpaceShipType)
            {
                case eSpaceShipType.Blue:
                    m_SpaceShip = SpritesFactory.CreateSprite(i_GameScreen, SpritesFactory.eSpriteType.BlueSpaceShip) as SpaceShip;
                    m_ScoreText.TintColor = Color.Blue;
                    lifeType = SpritesFactory.eSpriteType.LifeBlueSpaceShip;
                    break;

                case eSpaceShipType.Green:
                    m_SpaceShip = SpritesFactory.CreateSprite(i_GameScreen, SpritesFactory.eSpriteType.GreenSpaceShip) as SpaceShip;
                    m_ScoreText.TintColor = Color.Green;
                    lifeType = SpritesFactory.eSpriteType.LifeGreenSpaceShip;
                    break;
            }

            for (int i = 0; i < this.Lifes; i++)
            {
                Life life = SpritesFactory.CreateSprite(i_GameScreen, lifeType) as Life;
                life.Initialize();
                this.LifesSprites.Add(life);
            }
        }

        private Sound m_LoseLifeSound;

        public override void LoseLife()
        {
            if (Lifes > 0)
            {
                m_LoseLifeSound.Play();
                this.LifesSprites[Lifes - 1].Visible = false;
                base.LoseLife();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            m_ScoreText.TextString = string.Format("{0} Score: {1}", this.Nickname, this.Score);
        }
    }
}
