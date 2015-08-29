using C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuItems;
using C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuItems.RangeMenuItem;
using C15Ex03Dotan301810610Bar308000322.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C15Ex03Dotan301810610Bar308000322.Menu.ConcreteMenuScreens
{
    public class SoundOptionsScreen : MenuScreen
    {
        private SpaceInvadersSoundsMng m_SpaceInvadersSoundsManager;
    
        public SoundOptionsScreen(Game i_Game)
            : base(i_Game, "Sound Options")
        {
            m_SpaceInvadersSoundsManager = SpaceInvadersServices.GetSoundManager(this.Game);
        }
        protected override void InitMenuItems()
        {
            ToggleMenuItem toggleSoundItem = new ToggleMenuItem("Toggle Sound:", this, m_SpaceInvadersSoundsManager.SoundStatus, new List<string>() { "On", "Off" }, Keys.Enter,
                new MethodKey() { MethodToRun = m_SpaceInvadersSoundsManager.MuteAllVolumeInstances, ActivateKey = Microsoft.Xna.Framework.Input.Keys.PageDown },
                new MethodKey() { MethodToRun = m_SpaceInvadersSoundsManager.UnMuteAllVolumeInstances, ActivateKey = Microsoft.Xna.Framework.Input.Keys.PageUp });
            RangeMenuItem backgroundVolumeItem = new RangeMenuItem("Background Music Vol:", this, (int)(m_SpaceInvadersSoundsManager.BackgroundSound.Volume * 100), 0, 100, 10, 
                new MethodKey() { MethodToRun = m_SpaceInvadersSoundsManager.DecreaseBackgroundMusic, ActivateKey = Keys.PageUp }, 
                new MethodKey() { MethodToRun = m_SpaceInvadersSoundsManager.IncreaseBackgroundMusic, ActivateKey = Keys.PageDown });
            RangeMenuItem soundsEffectsVolumeItem = new RangeMenuItem("Sounds Effects Vol:", this, (int)(m_SpaceInvadersSoundsManager.SoundEffect.Volume * 100), 0, 100, 10, 
                new MethodKey() { MethodToRun = m_SpaceInvadersSoundsManager.DecreaseSoundEffect, ActivateKey = Keys.PageUp }, 
                new MethodKey() { MethodToRun = m_SpaceInvadersSoundsManager.IncreaseSoundEffect, ActivateKey = Keys.PageDown });
            TextMenuItem doneItem = new TextMenuItem("Done", this, new MethodKey() { MethodToRun = new SpaceInvadersMenuMethods(this.Game, SpaceInvadersMenuMethods.eMethodsToRun.Done).RunMethod, ActivateKey = Keys.Enter });
            AddMenuItems(toggleSoundItem, backgroundVolumeItem, soundsEffectsVolumeItem, doneItem);
        }
    }
}
