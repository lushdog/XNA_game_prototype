using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyFirstGame.LevelObject;

namespace MyFirstGame.GameObject
{
    abstract class Target : AIActor
    {
        public IPattern Pattern { get; set; }
        public int PointValue { get; set; }

        public Target()
        {
            
        }
    }
}
