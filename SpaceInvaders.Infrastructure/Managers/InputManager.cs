//*** Guy Ronen (c) 2008-2011 ***//
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders.Infrastructure.ObjectModels;
using SpaceInvaders.Infrastructure.ServiceInterfaces;
using System.Text;

namespace SpaceInvaders.Infrastructure.Managers
{
    public class InputManager : GameService, IInputManager
    {
        private KeyboardState m_PrevKeyboardState;
        public KeyboardState PrevKeyboardState
        {
            get { return m_PrevKeyboardState; }
        }

        private KeyboardState m_KeyboardState;
        public KeyboardState KeyboardState
        {
            get { return m_KeyboardState; }
        }

        private MouseState m_PrevMouseState;
        public MouseState PrevMouseState
        {
            get { return m_PrevMouseState; }
        }

        private MouseState m_MouseState;
        public MouseState MouseState
        {
            get { return m_MouseState; }
        }

        private GamePadState m_PrevGamePadState;
        public GamePadState PrevGamePadState
        {
            get { return m_PrevGamePadState; }
        }

        private GamePadState m_GamePadState;
        public GamePadState GamePadState
        {
            get { return m_GamePadState; }
        }

        public InputManager(Game i_Game)
            // we want this component to be updated first!
            : base(i_Game, int.MinValue)
        { }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            m_PrevKeyboardState = Keyboard.GetState();
            m_KeyboardState = m_PrevKeyboardState;

            m_PrevMouseState = Mouse.GetState();
            m_MouseState = m_PrevMouseState;

            m_PrevGamePadState = GamePad.GetState(PlayerIndex.One);
            m_GamePadState = m_PrevGamePadState;
        }

        protected override void RegisterAsService()
        {
            Game.Services.AddService(typeof(IInputManager), this);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            m_PrevKeyboardState = m_KeyboardState;
            m_KeyboardState = Keyboard.GetState();

            m_PrevMouseState = m_MouseState;
            m_MouseState = Mouse.GetState();

            m_PrevGamePadState = m_GamePadState;
            m_GamePadState = GamePad.GetState(PlayerIndex.One);
        }

        #region Keyboard Services
        /// <summary>
        /// Checks if the provided key was pressed for the last two frames.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Returns true if held.</returns>
        public bool KeyHeld(Keys i_Key)
        {
            return (m_KeyboardState.IsKeyDown(i_Key) && m_PrevKeyboardState.IsKeyDown(i_Key));
        }

        /// <summary>
        /// Checks if the provided key was released this frame.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Return true if so.</returns>
        public bool KeyReleased(Keys i_Key)
        {
            return (
                m_PrevKeyboardState.IsKeyDown(i_Key)
                &&
                m_KeyboardState.IsKeyUp(i_Key));
        }

        /// <summary>
        /// Checks if the provided key was pressed at this frame.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Return true if so.</returns>
        public bool KeyPressed(Keys i_Key)
        {
            return (m_PrevKeyboardState.IsKeyUp(i_Key) && m_KeyboardState.IsKeyDown(i_Key));
        }
        #endregion Keyboard Services

        /// <summary>
        /// Returns if the provided button(s) was PRESSED at this frame.
        /// </summary>
        /// <param name="i_Buttons">
        /// Buttons to query.
        /// Specify a single button, or combine multiple buttons using
        /// a bitwise OR operation.</param>
        /// <returns>
        /// true if one of the specified buttons (or more) was just PRESSED.
        /// false otherwise
        /// </returns>
        public bool ButtonPressed(eInputButtons i_Buttons)
        {
            const bool v_OneIsEnough = true;

            return ButtonStateChanged(i_Buttons, ButtonState.Pressed, v_OneIsEnough);
        }

        /// <summary>
        /// Returns if the provided button(s) was RELEASED at this frame.
        /// </summary>
        /// <param name="i_Buttons">
        /// Buttons to query.
        /// Specify a single button, or combine multiple buttons using
        /// a bitwise OR operation.</param>
        /// <returns>
        /// true if one of the specified buttons (or more) was just RELEASED.
        /// false otherwise
        /// </returns>
        public bool ButtonReleased(eInputButtons i_Buttons)
        {
            const bool v_OneIsEnough = true;

            return ButtonStateChanged(i_Buttons, ButtonState.Released, v_OneIsEnough);
        }

        /// <summary>
        /// Returns if the provided button(s) were PRESSED at this frame.
        /// </summary>
        /// <param name="i_Buttons">
        /// Buttons to query.
        /// Specify a single button, or combine multiple buttons using
        /// a bitwise OR operation.</param>
        /// <returns>
        /// true if all of the specified buttons were just PRESSED.
        /// false otherwise
        /// </returns>
        public bool ButtonsPressed(eInputButtons i_Buttons)
        {
            const bool v_OneIsEnough = true;

            return ButtonStateChanged(i_Buttons, ButtonState.Pressed, !v_OneIsEnough);
        }

        /// <summary>
        /// Returns if the provided button(s) were RELEASED at this frame.
        /// </summary>
        /// <param name="i_Buttons">
        /// Buttons to query.
        /// Specify a single button, or combine multiple buttons using
        /// a bitwise OR operation.</param>
        /// <returns>
        /// true if all of the specified buttons were just RELEASED.
        /// false otherwise
        /// </returns>
        public bool ButtonsReleased(eInputButtons i_Buttons)
        {
            const bool v_OneIsEnough = true;

            return ButtonStateChanged(i_Buttons, ButtonState.Released, !v_OneIsEnough);
        }

        /// <summary>
        /// Returns if the provided button(s) state was just changed in this frame (release->pressed / pressed->release)
        /// </summary>
        /// <param name="i_Buttons">
        /// Buttons to query.
        /// Specify a single button, or combine multiple buttons using
        /// a bitwise OR operation.</param>
        /// <returns>
        /// true if one of the specified buttons state was just changed (release->pressed / pressed->release).
        /// false otherwise
        /// </returns>
        public bool ButtonStateChanged(eInputButtons i_Buttons)
        {
            const bool v_OneIsEnough = true;

            return ButtonStateChanged(i_Buttons, ButtonState.Released, v_OneIsEnough)
                ||
                ButtonStateChanged(i_Buttons, ButtonState.Pressed, v_OneIsEnough);
        }

        private bool ButtonStateChanged(eInputButtons i_Buttons, ButtonState i_ButtonState, bool i_IsOneEnough)
        {
            const bool v_CheckChanged = true;

            return checkButtonsState(i_Buttons, i_ButtonState, i_IsOneEnough, v_CheckChanged);
        }

        private bool checkButtonsState(eInputButtons i_Buttons, ButtonState i_ButtonState, bool i_IsOneEnough)
        {
            const bool v_CheckChanged = true;
            return checkButtonsState(i_Buttons, i_ButtonState, i_IsOneEnough, !v_CheckChanged);
        }

        private bool checkButtonsState(eInputButtons i_Buttons, ButtonState i_ButtonState, bool i_IsOneEnough, bool i_CheckChanged)
        {
            bool checkRelease = i_ButtonState == ButtonState.Released;

            bool atLeastOneIsTrue = false;
            bool allTrue = false;
            bool currCheck = false;

            ButtonState currState = i_ButtonState;
            ButtonState prevState = checkRelease ? ButtonState.Pressed : ButtonState.Released;

            #region GamePad Controls
            if ((i_Buttons & eInputButtons.A) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.A)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.A));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.B) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.B)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.B));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.X) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.X)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.X));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.Y) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.Y)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.Y));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.DPadDown) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.DPadDown)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.DPadDown));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.DPadUp) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.DPadUp)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.DPadUp));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.DPadLeft) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.DPadLeft)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.DPadLeft));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.DPadRight) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.DPadRight)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.DPadRight));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.Back) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.Back)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.Back));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.Start) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.Start)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.Start));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }

            if ((i_Buttons & eInputButtons.LeftShoulder) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.LeftShoulder)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.LeftShoulder));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }

            if ((i_Buttons & eInputButtons.RightShoulder) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.RightShoulder)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.RightShoulder));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }

            if ((i_Buttons & eInputButtons.LeftStick) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.LeftStick)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.LeftStick));
                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.RightStick) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.RightStick)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.RightStick));
                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.LeftThumbstickDown) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.LeftThumbstickDown)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.LeftThumbstickDown));
                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.LeftThumbstickUp) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.LeftThumbstickUp)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.LeftThumbstickUp));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.LeftThumbstickLeft) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.LeftThumbstickLeft)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.LeftThumbstickLeft));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.LeftThumbstickRight) != 0)
            {
                currCheck = checkRelease == m_GamePadState.IsButtonUp(Buttons.LeftThumbstickRight)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.LeftThumbstickRight));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.RightThumbstickDown) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.RightThumbstickDown)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.RightThumbstickDown));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.RightThumbstickUp) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.RightThumbstickUp)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.RightThumbstickUp));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.RightThumbstickLeft) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.RightThumbstickLeft)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.RightThumbstickLeft));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.RightThumbstickRight) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.RightThumbstickRight)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.RightThumbstickRight));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.LeftTrigger) != 0)
            {
                currCheck =
                    checkRelease == m_GamePadState.IsButtonUp(Buttons.LeftTrigger)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.LeftTrigger));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            if ((i_Buttons & eInputButtons.RightTrigger) != 0)
            {
                currCheck =
                     checkRelease == m_GamePadState.IsButtonUp(Buttons.RightTrigger)
                    && (!i_CheckChanged || checkRelease != m_PrevGamePadState.IsButtonUp(Buttons.RightTrigger));

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            #endregion GamePad Controls

            #region Mouse Buttons
            if ((i_Buttons & eInputButtons.Left) != 0)
            {
                currCheck =
                    m_MouseState.LeftButton == currState
                    && ((m_PrevMouseState.LeftButton == prevState) || !i_CheckChanged);

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            else if ((i_Buttons & eInputButtons.Middle) != 0)
            {
                currCheck =
                    m_MouseState.MiddleButton == currState
                    && ((m_PrevMouseState.MiddleButton == prevState) || !i_CheckChanged);

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            else if ((i_Buttons & eInputButtons.Right) != 0)
            {
                currCheck =
                    m_MouseState.RightButton == currState
                    && ((m_PrevMouseState.RightButton == prevState) || !i_CheckChanged);

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            else if ((i_Buttons & eInputButtons.XButton1) != 0)
            {
                currCheck =
                    m_MouseState.XButton1 == currState
                    && ((m_PrevMouseState.XButton1 == prevState) || !i_CheckChanged);

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            else if ((i_Buttons & eInputButtons.XButton2) != 0)
            {
                currCheck =
                    m_MouseState.XButton2 == currState
                    && ((m_PrevMouseState.XButton2 == prevState) || !i_CheckChanged);

                atLeastOneIsTrue |= currCheck;
                allTrue &= currCheck;
            }
            #endregion Mouse Buttons

            return i_IsOneEnough ? atLeastOneIsTrue : allTrue;

        }

        /// <summary>
        /// Returns if one (or more) of the provided button(s) are down at this frame
        /// </summary>
        /// <param name="button"></param>
        /// <returns>
        /// true if one of the specified buttons is currently down.
        /// false otherwise
        /// </returns>
        public bool ButtonIsDown(eInputButtons i_MouseButtons)
        {
            const bool v_OneIsEnough = true;
            return checkButtonsState(i_MouseButtons, ButtonState.Pressed, v_OneIsEnough);
        }

        /// <summary>
        /// Returns if all of the provided buttons are down at this frame
        /// </summary>
        /// <param name="button"></param>
        /// <returns>
        /// true if all of the specified buttons are currently down.
        /// false otherwise
        /// </returns>
        public bool ButtonsAreDown(eInputButtons i_MouseButtons)
        {
            const bool v_OneIsEnough = true;
            return checkButtonsState(i_MouseButtons, ButtonState.Pressed, !v_OneIsEnough);
        }

        /// <summary>
        /// Returns if one (or more) of the provided button(s) are up at this frame
        /// </summary>
        /// <param name="button"></param>
        /// <returns>
        /// true if one of the specified buttons is currently up.
        /// false otherwise
        /// </returns>
        public bool ButtonIsUp(eInputButtons i_MouseButtons)
        {
            const bool v_OneIsEnough = true;
            return checkButtonsState(i_MouseButtons, ButtonState.Released, v_OneIsEnough);
        }

        /// <summary>
        /// Returns if all of the provided buttons are up at this frame
        /// </summary>
        /// <param name="button"></param>
        /// <returns>
        /// true if all of the specified buttons are currently up.
        /// false otherwise
        /// </returns>
        public bool ButtonsAreUp(eInputButtons i_MouseButtons)
        {
            const bool v_OneIsEnough = true;
            return checkButtonsState(i_MouseButtons, ButtonState.Released, !v_OneIsEnough);
        }

        public Vector2 MousePositionDelta
        {
            get
            {
                return new Vector2(
                    (float)(m_MouseState.X - m_PrevMouseState.X),
                    (float)(m_MouseState.Y - m_PrevMouseState.Y));
            }
        }

        public int 
            
            
            WheelDelta
        {
            get { return m_MouseState.ScrollWheelValue - m_PrevMouseState.ScrollWheelValue; }
        }

        public Vector2 LeftThumbDelta
        {
            get
            {
                return new Vector2(
                    m_GamePadState.ThumbSticks.Left.X - m_PrevGamePadState.ThumbSticks.Left.X,
                    m_GamePadState.ThumbSticks.Left.Y - m_PrevGamePadState.ThumbSticks.Left.Y);
            }
        }

        public Vector2 RightThumbDelta
        {
            get
            {
                return new Vector2(
                    m_GamePadState.ThumbSticks.Right.X - m_PrevGamePadState.ThumbSticks.Right.X,
                    m_GamePadState.ThumbSticks.Right.Y - m_PrevGamePadState.ThumbSticks.Right.Y);
            }
        }

        public float LeftTrigerDelta
        {
            get { return m_GamePadState.Triggers.Left - m_PrevGamePadState.Triggers.Left; }
        }

        public float RightTrigerDelta
        {
            get { return m_GamePadState.Triggers.Right - m_PrevGamePadState.Triggers.Right; }
        }

        public string PressedKeys
        {
            get
            {
                Keys[] pressedKeys = m_KeyboardState.GetPressedKeys();
                string keys = string.Empty;

                if (pressedKeys.Length > 0)
                {
                    StringBuilder keysMsgBuilder = new StringBuilder(pressedKeys.Length * 3);
                    int keysCount = 0;
                    foreach (Keys key in pressedKeys)
                    {
                        keysCount++;
                        keysMsgBuilder.Append(key.ToString());
                        if (keysCount < pressedKeys.Length)
                        {
                            keysMsgBuilder.Append(", ");
                        }
                    }

                    keys = keysMsgBuilder.ToString();
                }

                return keys;
            }
        }

        public override string ToString()
        {
            string status = string.Format(@"
Keyboard.PressedKeys:       {18}

GamePad.IsConnected:        {0}
GamePad.ThumbSticks.Left    {1}
GamePad.ThumbSticks.Right:  {2}
GamePad.Triggers.Left:      {3}
GamePad.Triggers.Right:     {4}
GamePad.DPad:               {5}
GamePad.Buttons:            {6}
GamePad.PacketNumber:       {7}

Mouse.X:            {8}
Mouse.Y:            {9}
Mouse.DeltaXY:      {10}
Mouse.Left:         {11}
Mouse.Middle:       {12}
Mouse.Right:        {13}
Mouse.XButton1:     {14}
Mouse.XButton2:     {15}
ScrollWheelValue:   {16}
ScrollWheelDelta:   {17}
",
 m_GamePadState.IsConnected,
 m_GamePadState.ThumbSticks.Left,
 m_GamePadState.ThumbSticks.Right,
 m_GamePadState.Triggers.Left,
 m_GamePadState.Triggers.Right,
 m_GamePadState.DPad,
 m_GamePadState.Buttons,
 m_GamePadState.PacketNumber,

 m_MouseState.X,
 m_MouseState.Y,
 MousePositionDelta,
 m_MouseState.LeftButton,
 m_MouseState.MiddleButton,
 m_MouseState.RightButton,
 m_MouseState.XButton1,
 m_MouseState.XButton2,
 m_MouseState.ScrollWheelValue,
 ScrollWheelDelta,
 PressedKeys
 );
            return status;
        }

        public bool KeyPressed(Keys[] i_Keys)
        {
            bool pressed = false;
            foreach (Keys key in i_Keys)
            {
                if (this.KeyPressed(key))
                {
                    pressed = true;
                    break;
                }
            }
            return pressed;
        }


        public int ScrollWheelDelta
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}