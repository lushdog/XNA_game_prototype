using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyFirstGame.GameObject
{
    abstract class AIActor : Actor
    {
        private Vector2 _origin;
        
        public override Vector2 Origin
        {
            get
            {
                return new Vector2(Sprite.Width / 2, Sprite.Height / 2);  
            }
            set
            {
                _origin = value;
            }

        }
        public bool IsActive { get; set; }
        public string SpritePath { get; set; }
        public Texture2D Sprite { get; set; }
        public int PointValue { get; set; }       
        public Rectangle BoundingBox 
        {
            get
            {
                //TODO: this will not work if the origin is not in the center of the sprite
                return new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), Sprite.Width, Sprite.Height);
            }            
        }

        public AIActor()
        {
            base.Rotation = 0.0f;                      
        }
    }
}
