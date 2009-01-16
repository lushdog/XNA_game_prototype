using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using MyFirstGame.GameInput;

namespace MyFirstGame.GameObject
{
    class PlayerActor : Actor
    {
        private Texture2D _sprite;
        private PlayerInput _activeInput;
        
        public Texture2D Sprite
        {
            get
            {
                return _sprite;
            }
            set
            {
                _sprite = value;
            }
        }

        public PlayerInput ActiveInput
        {
            get
            {
                return _activeInput;
            }
            set
            {
                _activeInput = value;
            }
        }

        public PlayerActor(PlayerInput activeInput, Texture2D sprite)
        {
            _activeInput = activeInput;
            _sprite = sprite;
            base.Visible = true;
            base.Rotation = 0.0f;
        }

        public void UpdateInput(int viewportWidth, int viewportHeight)
        {
            float inputX = ActiveInput.GetX(); 
            float inputY = ActiveInput.GetY();

            float newPosX = 0.0f;
            float newPosY = 0.0f;

            if (ActiveInput is GamepadInput)
            {
                newPosX = this.Position.X + (inputX * ((GamepadInput)ActiveInput).ScrollSpeed);
                newPosY = this.Position.Y - (inputY * ((GamepadInput)ActiveInput).ScrollSpeed);
            }
#if !XBOX            
            else if (ActiveInput is KeyboardInput)
            {
                newPosX = this.Position.X - (inputX * ((KeyboardInput)ActiveInput).ScrollSpeed);
                newPosY = this.Position.Y - (inputY * ((KeyboardInput)ActiveInput).ScrollSpeed);
            }
            else if (ActiveInput is MouseInput)
            {
                newPosX = inputX;
                newPosY = inputY;
            }
            else if (ActiveInput is WiiInput)
            {
                newPosX = inputX * (float)viewportWidth;
                newPosY = inputY * (float)viewportHeight;
            }
#endif
            newPosX = MathHelper.Clamp(newPosX, 0.0f, viewportWidth);
            newPosY = MathHelper.Clamp(newPosY, 0.0f, viewportHeight);
            this.MoveTo((int)newPosX, (int)newPosY);

#if DEBUG
            string playerNumber = "Player " + ActiveInput.PlayerNumber.ToString();
            Console.WriteLine(playerNumber + " MoveX = " + inputX.ToString());
            Console.WriteLine(playerNumber + " MoveY = " + inputY.ToString());
            //Console.WriteLine(playerNumber + " PosX = " + posX.ToString());
            //Console.WriteLine(playerNumber + " PosY = " + posY.ToString());
            //Console.WriteLine(playerNumber + " CrosshairX = " + crosshair.position.X.ToString());
            //Console.WriteLine(playerNumber + " CrosshairY = " + crosshair.position.Y.ToString());
#endif
        }

    }
}
