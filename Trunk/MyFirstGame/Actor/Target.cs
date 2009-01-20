using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyFirstGame.GameObject
{
    abstract class Target : AIActor
    {
        public int PointValue { get; set; }
        
        public Target(Vector2 maxPosition) : base(maxPosition)
        {
            
        }
    }
}
