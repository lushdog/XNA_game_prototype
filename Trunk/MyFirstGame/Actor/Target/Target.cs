using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyFirstGame.LevelObject;

namespace MyFirstGame.GameObject
{
    abstract class Target : Sprite
    {
        public bool IsActive { get; set; }
        public int PointValue { get; set; }
        public Rectangle BoundingBox
        {
            get
            {
                //TODO: this will not work if the origin is not in the center of the sprite
                return new Rectangle((int)(Position.X - Origin.X), (int)(Position.Y - Origin.Y), DrawRectangle.Width, DrawRectangle.Height);
            }
        }        
        public Pattern Pattern { get; set; }
        public Target()
        {
            
        }
    }
}
