using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyFirstGame.GameObject
{
    abstract class EnemySprite : Sprite
    {
        private Vector2 _origin;
        
        public bool IsActive { get; set; }
        public int PointValue { get; set; }       
        public Rectangle BoundingBox 
        {
            get
            {
                //TODO: this will not work if the origin is not in the center of the sprite
                return new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), SpriteTexture.Width, SpriteTexture.Height);
            }            
        }

        public EnemySprite()
        {
            base.Rotation = 0.0f;                      
        }
    }
}
