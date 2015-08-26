//*** Guy Ronen (c) 2008-2011 ***//
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Infrastructure.ServiceInterfaces
{
    [Flags]
    public enum eInputButtons
    {
        // Mouse buttons:
        Left = 65536,
        Middle = 131072,
        Right = 262144,
        XButton1 = 524288,
        XButton2 = 1048576,

        // GamePad buttons:
        DPadUp = Buttons.DPadUp,
        DPadDown = Buttons.DPadDown,
        DPadLeft = Buttons.DPadLeft,
        DPadRight = Buttons.DPadRight,
        Start = Buttons.Start,
        Back = Buttons.Back,
        LeftStick = Buttons.LeftStick,
        RightStick = Buttons.RightStick,
        LeftShoulder = Buttons.LeftShoulder,
        RightShoulder = Buttons.RightShoulder,
        A = Buttons.A,
        B = Buttons.B,
        X = Buttons.X,
        Y = Buttons.Y,

        LeftThumbstickLeft = Buttons.LeftThumbstickLeft,
        RightTrigger = Buttons.RightTrigger,
        LeftTrigger = Buttons.LeftTrigger,
        RightThumbstickUp = Buttons.RightThumbstickUp,
        RightThumbstickDown = Buttons.RightThumbstickDown,
        RightThumbstickRight = Buttons.RightThumbstickRight,
        RightThumbstickLeft = Buttons.RightThumbstickLeft,
        LeftThumbstickUp = Buttons.LeftThumbstickUp,
        LeftThumbstickDown = Buttons.LeftThumbstickDown,
        LeftThumbstickRight = Buttons.LeftThumbstickRight,
    };

    public interface IInputManager
    {
        // Exposes the three input device states as well:
        GamePadState GamePadState { get; }
        KeyboardState KeyboardState { get; }
        MouseState MouseState { get; }


        // Allows querying buttons current state (Mouse and GamePad):
        bool ButtonIsDown(eInputButtons i_MouseButtons);

        bool ButtonIsUp(eInputButtons i_MouseButtons);

        bool ButtonsAreDown(eInputButtons i_MouseButtons);

        bool ButtonsAreUp(eInputButtons i_MouseButtons);

        // Allows querying buttons state CHANGES (Mouse and GamePad):
        bool ButtonPressed(eInputButtons i_Buttons);

        bool ButtonReleased(eInputButtons i_Buttons);

        bool ButtonsPressed(eInputButtons i_Buttons);

        bool ButtonsReleased(eInputButtons i_Buttons);

        // Allows querying KEYBOARD's state CHANGES:
        bool KeyPressed(Keys i_Key);
        bool KeyPressed(Keys[] i_Keys);


        bool KeyReleased(Keys i_Key);

        bool KeyHeld(Keys i_Key);

        // Allows querying all kind of analog input DELTAs:
        Vector2 MousePositionDelta { get; }
        int ScrollWheelDelta { get; }
        Vector2 LeftThumbDelta { get; }
        Vector2 RightThumbDelta { get; }
        float LeftTrigerDelta { get; }
        float RightTrigerDelta { get; }
    }
}
