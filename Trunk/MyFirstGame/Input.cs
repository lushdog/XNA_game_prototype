using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
#if !XBOX
    using WiimoteLib;
#endif

namespace MyFirstGame
{
    public abstract class Input
    {
        public abstract float GetX(); 
        public abstract float GetY();
        public abstract bool GetFire();
        public abstract bool GetExit();
    }

#if !XBOX

    public class KeyboardInput : Input
    {
        public KeyboardInput()
        {
             
        }

        public override float GetY()
        {
            KeyboardState _keyboardState = Keyboard.GetState(PlayerIndex.One);
            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                return 1.0f;
            }
            else if (_keyboardState.IsKeyDown(Keys.Down))
            {
                return -1.0f;
            }
            else
            {
                return 0.0f;
            }
        }

        public override float GetX()
        {
            KeyboardState _keyboardState = Keyboard.GetState(PlayerIndex.One);
            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                return 1.0f;
            }
            else if (_keyboardState.IsKeyDown(Keys.Right))
            {
                return -1.0f;
            }
            else
            {
                return 0.0f;
            }
        }

        public override bool GetFire()
        {
            throw new NotImplementedException();
        }

        public override bool GetExit()
        {
            throw new NotImplementedException();
        }
    }

    public class WiiInput : Input
    {
        Wiimote _wiimote;
        
        public WiiInput()
        {
            _wiimote = InitWiimote();
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
            IRState _wiimoteIRState = _wiimote.WiimoteState.IRState;
            return _wiimoteIRState.Midpoint.Y;
        }

        public override float GetX()
        {
            IRState _wiimoteIRState = _wiimote.WiimoteState.IRState;
            return 1.0f - _wiimoteIRState.Midpoint.X;
        }

        public override bool GetFire()
        {
            throw new NotImplementedException();
        }

        public override bool GetExit()
        {
            throw new NotImplementedException();
        }

        private Wiimote InitWiimote()
        {
            WiimoteCollection wiimoteCollection = new WiimoteCollection();
            Wiimote wiimote;
            int index = 1;

            //safe to say that only one Wiimote connected to PC
            try
            {
                wiimoteCollection.FindAllWiimotes();
                wiimote = wiimoteCollection.ElementAt<Wiimote>(0);
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
                    wiimote.SetLEDs(index++);
                }
                catch
                {
                    wiimote = null;
                }
            }

            return wiimote;
        }
    }

    public class MouseInput : Input
    {
        public MouseInput()
        {

        }

        public override float GetY()
        {
            MouseState _mouseState = Mouse.GetState();
            return _mouseState.Y;
        }

        public override float GetX()
        {
            MouseState _mouseState = Mouse.GetState();
            return _mouseState.X;
        }

        public override bool GetFire()
        {
            throw new NotImplementedException();
        }

        public override bool GetExit()
        {
            throw new NotImplementedException();
        }
    }

#endif

    public class GamepadInput : Input
    {
        public GamepadInput()
        {

        }

        public override float GetY()
        {
            GamePadState _gamePadState = GamePad.GetState(PlayerIndex.One);
            return _gamePadState.ThumbSticks.Left.Y;
        }

        public override float GetX()
        {
            GamePadState _gamePadState = GamePad.GetState(PlayerIndex.One);
            return _gamePadState.ThumbSticks.Left.X;
        }

        public override bool GetFire()
        {
            throw new NotImplementedException();
        }

        public override bool GetExit()
        {
            throw new NotImplementedException();
        }
    }
}

