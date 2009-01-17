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
    public enum PlayerActorState { Firing, Active, Paused, Reloading, Foo }
    
    class PlayerActor : Actor
    {
        private Texture2D _sprite;
        private PlayerInput _activeInput;
        private int _playerNumber;
        private List<PlayerActorState> _playerActorStates;

        public int PlayerNumber
        {
            get
            {
                return _playerNumber;
            }
            set
            {
                _playerNumber = value;
            }
        }        
        
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

        public List<PlayerActorState> PlayerActorStates
        {
            get
            {
                return _playerActorStates;
            }
            set
            {
                _playerActorStates = value;
            }
        }

        public PlayerActor(PlayerInput activeInput, Texture2D sprite, int playerNumber, Vector2 maxPosition) : base (maxPosition)
        {
            _activeInput = activeInput;
            _sprite = sprite;
            _playerNumber = playerNumber;
            _playerActorStates = new List<PlayerActorState>();

            base.Visible = true;
            base.Rotation = 0.0f;
            base.Origin = new Vector2(_sprite.Width / 2, _sprite.Height / 2);            
        }

        public void UpdatePlayerIsActive()
        {
            if (!PlayerActorStates.Contains(PlayerActorState.Active))
            {
                if (ActiveInput.GetFire())
                {
                    PlayerActorStates.Add(PlayerActorState.Active);
                }
            }
        }

        public void UpdatePlayerPosition()
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
                newPosX = inputX * (float)MaxPosition.X;
                newPosY = inputY * (float)MaxPosition.Y;
            }
#endif
            newPosX = MathHelper.Clamp(newPosX, 0.0f, MaxPosition.X);
            newPosY = MathHelper.Clamp(newPosY, 0.0f, MaxPosition.Y);
            this.Position = new Vector2(newPosX, newPosY);
        }

        public void UpdateFiringState()
        {
            if (ActiveInput.GetFire())
            {
                if (!PlayerActorStates.Contains(PlayerActorState.Firing))
                {
                    PlayerActorStates.Add(PlayerActorState.Firing);
                }                
            }
            else
            {
                PlayerActorStates.Remove(PlayerActorState.Firing);
            }
        }

        public void UpdatePauseState()
        {
            if (ActiveInput.GetPause())
            {
                if (!PlayerActorStates.Contains(PlayerActorState.Paused))
                {
                    PlayerActorStates.Add(PlayerActorState.Paused);
                }
            }
            else
            {
                PlayerActorStates.Remove(PlayerActorState.Paused);
            }
        }

    }
}
