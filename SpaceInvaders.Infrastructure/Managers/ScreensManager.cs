using Microsoft.Xna.Framework;
using SpaceInvaders.Infrastructure.ObjectModel;
using SpaceInvaders.Infrastructure.ObjectModel.Screens;
using SpaceInvaders.Infrastructure.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders.Infrastructure.Managers
{
    public class ScreensManager : CompositeDrawableComponent<GameScreen>, IScreensManager
    {
        public ScreensManager(Game i_Game)
            : base(i_Game)
        {
            i_Game.Components.Add(this);
        }

        private Stack<GameScreen> m_ScreensStack = new Stack<GameScreen>();

        public GameScreen ActiveScreen
        {
            get { return m_ScreensStack.Count > 0 ? m_ScreensStack.Peek() : null; }
        }

        public void SetCurrentScreen(GameScreen i_GameScreen)
        {
            Push(i_GameScreen);

            i_GameScreen.Activate();
        }

        public void Push(GameScreen i_GameScreen)
        {
            // hello new screen, I am your manager, nice to meet you:
            i_GameScreen.ScreensManager = this;

            if (!this.Contains(i_GameScreen))
            {
                this.Add(i_GameScreen);

                // let me know when you are closed, so i can pop you from the stack:
                i_GameScreen.StateChanged += Screen_StateChanged;
            }

            if (ActiveScreen != i_GameScreen)
            {
                if (ActiveScreen != null)
                {
                    // connect each new screen to the previous one:
                    i_GameScreen.PreviousScreen = ActiveScreen;

                    ActiveScreen.Deactivate();
                }
            }

            if (ActiveScreen != i_GameScreen)
            {
                m_ScreensStack.Push(i_GameScreen);
            }

            i_GameScreen.DrawOrder = m_ScreensStack.Count;
        }

        private void Screen_StateChanged(object sender, StateChangedEventArgs e)
        {
            switch (e.CurrentState)
            {
                case eScreenState.Activating:
                    break;
                case eScreenState.Active:
                    break;
                case eScreenState.Deactivating:
                    break;
                case eScreenState.Closing:
                    Pop(sender as GameScreen);
                    break;
                case eScreenState.Inactive:
                    break;
                case eScreenState.Closed:
                    Remove(sender as GameScreen);
                    break;
                default:
                    break;
            }

            OnScreenStateChanged(sender, e);
        }

        private void Pop(GameScreen i_GameScreen)
        {
            m_ScreensStack.Pop();

            if (m_ScreensStack.Count > 0)
            {
                // when one is popped, the previous becomes the active one
                ActiveScreen.Activate();
            }
        }

        private new bool Remove(GameScreen i_Screen)
        {
            return base.Remove(i_Screen);
        }

        private new void Add(GameScreen i_Component)
        {
            base.Add(i_Component);
        }

        public event EventHandler<StateChangedEventArgs> ScreenStateChanged;
        protected virtual void OnScreenStateChanged(object sender, StateChangedEventArgs e)
        {
            if (ScreenStateChanged != null)
            {
                ScreenStateChanged(sender, e);
            }
        }

        protected override void OnComponentRemoved(GameComponentEventArgs<GameScreen> e)
        {
            base.OnComponentRemoved(e);

            e.GameComponent.StateChanged -= Screen_StateChanged;

            if (m_ScreensStack.Count == 0)
            {
                Game.Exit();
            }
        }

        public override void Initialize()
        {
            Game.Services.AddService(typeof(IScreensManager), this);

            base.Initialize();
        }
    }
}
