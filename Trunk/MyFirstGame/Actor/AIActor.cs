using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyFirstGame.GameObject
{
    public enum AIActorState { Active }
    
    abstract class AIActor : Actor
    {
        private int _pointValue;
        private List<AIActorState> _aiActorStates;
        private string _spritePath;
        private Texture2D _sprite;
        private Vector2 _origin;

        public override Vector2 Origin
        {
            get
            {
                return new Vector2(_sprite.Width / 2, _sprite.Height / 2);  
            }
            set
            {
                _origin = value;
            }

        }

        public string SpritePath 
        {
            get
            {
                return _spritePath;
            }
            set
            {
                _spritePath = value;
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

        public int PointValue 
        {
            get
            {
                return _pointValue;
            }
            set
            {
                _pointValue = value;
            }
        }

        public Rectangle BoundingBox 
        {
            get
            {
                //TODO: this will not work if the origin is not in the center of the sprite
                return new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), Sprite.Width, Sprite.Height);
            }            
        }

        public List<AIActorState> AIActorStates
        {
            get
            {
                return _aiActorStates;
            }
            set
            {
                _aiActorStates = value;
            }
        }
        
        public AIActor()
        {
            _aiActorStates = new List<AIActorState>();
            base.Rotation = 0.0f;                      
        }
    }
}
