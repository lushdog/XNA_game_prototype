using System.Linq;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

#if !XBOX
    using WiimoteLib;
#endif

namespace MyFirstGame.InputObject
{
    public abstract class PlayerInput
    {
        public PlayerInput()
        {

        }

        public abstract float GetX(); 
        public abstract float GetY();
        public abstract bool GetFire();
        public abstract bool GetPause();
    }

#if !XBOX

    public class KeyboardInput : PlayerInput
    {
        public float ScrollSpeed { get; set; }
        
        public KeyboardInput(float scrollSpeed)
        {
            ScrollSpeed = scrollSpeed;
        }

        public override float GetY()
        {
            KeyboardState _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                return -1.0f;
            }
            else if (_keyboardState.IsKeyDown(Keys.Down))
            {
                return +1.0f;
            }
            else
            {
                return 0.0f;
            }
        }

        public override float GetX()
        {
            KeyboardState _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                return -1.0f;
            }
            else if (_keyboardState.IsKeyDown(Keys.Right))
            {
                return 1.0f;
            }
            else
            {
                return 0.0f;
            }
        }

        public override bool GetFire()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Space);            
        }

        public override bool GetPause()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Escape);  
        }
    }

    public class WiiInput : PlayerInput
    {
        Wiimote Wiimote {get; set;}

        public WiiInput(int wiimoteNumber)
        {
            Wiimote = InitWiimote(wiimoteNumber);
        }

        //resolve IR pointer co-ords and screen co-ords
        //0,0 on screen
        //   0 on IRState.Y
        //|-------------------|
        //|                   |
        //|                   | //0 on IRState.X
        //|                   |
        //|-------------------|

        public override float GetY()
        {
            IRState _wiimoteIRState = Wiimote.WiimoteState.IRState;
            return _wiimoteIRState.Midpoint.Y;
        }

        public override float GetX()
        {
            IRState _wiimoteIRState = Wiimote.WiimoteState.IRState;
            return 1.0f - _wiimoteIRState.Midpoint.X;
        }

        public override bool GetFire()
        {
            return Wiimote.WiimoteState.ButtonState.B;
        }

        public override bool GetPause()
        {
            return Wiimote.WiimoteState.ButtonState.Home;
        }

        private Wiimote InitWiimote(int wiimoteIndex)
        {
            WiimoteCollection wiimoteCollection = new WiimoteCollection();
            Wiimote wiimote;
            
            try
            {
                wiimoteCollection.FindAllWiimotes();
                wiimote = wiimoteCollection.ElementAt<Wiimote>(wiimoteIndex);
            }
            catch
            {
                wiimote = null;
            }

            if (wiimote != null)
            {
                try
                {
                    wiimote.Connect();
                    wiimote.SetReportType(InputReport.IRExtensionAccel, IRSensitivity.Maximum, true);
                    wiimote.SetLEDs(wiimoteIndex + 1);
                }
                catch
                {
                    wiimote = null;
                }
            }

            return wiimote;
        }
    }

    public class MouseInput : PlayerInput
    {
        public MouseInput()
        {

        }

        public override float GetY()
        {
            return Mouse.GetState().Y;
        }

        public override float GetX()
        {
            return Mouse.GetState().X;
        }

        public override bool GetFire()
        {
            if (Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public override bool GetPause()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Escape);
        }
    }

#endif

    public class GamepadInput : PlayerInput
    {
        public float ScrollSpeed { get; set;}
        public PlayerIndex GamepadNumber { get; set; }

        public GamepadInput(PlayerIndex gamepadNumber, float scrollSpeed)
        {
            ScrollSpeed = scrollSpeed;
            GamepadNumber = gamepadNumber;
        }

        public override float GetY()
        {
            return GamePad.GetState(GamepadNumber).ThumbSticks.Left.Y;
        }

        public override float GetX()
        {
            return GamePad.GetState(GamepadNumber).ThumbSticks.Left.X;
        }

        public override bool GetFire()
        {
            return (GamePad.GetState(GamepadNumber).Triggers.Right > 0.9f);
        }

        public override bool GetPause()
        {
            return (GamePad.GetState(GamepadNumber).Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Pressed);
        }
    }
}

