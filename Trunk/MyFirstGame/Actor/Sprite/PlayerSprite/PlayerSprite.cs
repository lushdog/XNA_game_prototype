using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyFirstGame.InputObject;
using MyFirstGame.References;

namespace MyFirstGame.GameObject
{
    class PlayerSprite : Sprite
    {
        public bool IsFiring { get; set; }
        public bool IsActive { get; set; }
        public bool IsPaused { get; set; }
        
        public PlayerInput ActiveInput { get; set; }
        public int PlayerNumber { get; set; }
        
        public PlayerSprite(PlayerInput activeInput, Color spriteColor, int playerNumber)
        {            
            SpriteColor = spriteColor;
            ActiveInput = activeInput;
            PlayerNumber = playerNumber;
            base.Rotation = 0.0f;
            base.AnimationFrameCount = 1;
            base.AnimationFramesPerSecond = 0;
            base.AnimationStartName = "crosshair";
        }

        public void UpdatePlayerIsActive()
        {
            if (!this.IsActive)
            {
                if (ActiveInput.GetFire())
                {
                    this.IsActive = true;
                }
            }
        }

        public void UpdatePlayerPosition()
        {
            float inputX = ActiveInput.GetX();
            float inputY = ActiveInput.GetY();
            
            float newPosX = 0.0f;
            float newPosY = 0.0f;

            float time = (float)Settings.Instance.GameTime.ElapsedGameTime.TotalSeconds;
            float speed;
            float distanceX;
            float distanceY;

            if (ActiveInput is GamepadInput)
            {
                speed = ((GamepadInput)ActiveInput).ScrollSpeed;
                distanceX = time * inputX * speed;
                distanceY = time * inputY * speed;
                newPosX = this.Position.X + (distanceX);
                newPosY = this.Position.Y - (distanceY);
                
            }
#if !XBOX            
            else if (ActiveInput is KeyboardInput)
            {
                speed = ((KeyboardInput)ActiveInput).ScrollSpeed;
                distanceX = time * inputX * speed;
                distanceY = time * inputY * speed;
                newPosX = this.Position.X + (distanceX);
                newPosY = this.Position.Y + (distanceY);
            }
            else if (ActiveInput is MouseInput)
            {
                newPosX = inputX;
                newPosY = inputY;
            }
            else if (ActiveInput is WiiInput)
            {
                newPosX = inputX * (float)Settings.Instance.ScreenSize.X;
                newPosY = inputY * (float)Settings.Instance.ScreenSize.Y;
            }
#endif
            newPosX = MathHelper.Clamp(newPosX, 0.0f, Settings.Instance.ScreenSize.X);
            newPosY = MathHelper.Clamp(newPosY, 0.0f, Settings.Instance.ScreenSize.Y);
            this.Position = new Vector2(newPosX, newPosY);
        }

        //TODO: you can hold the button to fire, it should have to fire, then reset then fire again
        public void UpdateFiringState()
        {
            if (ActiveInput.GetFire())
            {
                if (!this.IsFiring)
                {
                    this.IsFiring = true;
                }                
            }
            else
            {
                this.IsFiring = false;
            }
        }

        public void UpdatePauseState()
        {
            if (ActiveInput.GetPause())
            {
                if (!this.IsPaused)
                {
                    this.IsPaused = true;
                }
            }
            else
            {
                this.IsPaused = false;
            }
        }

    }
}
