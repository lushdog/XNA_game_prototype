using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyFirstGame.References;

namespace MyFirstGame.GameObject
{
    class AlienTarget : Target
    {
        public AlienTarget()
        {
            base.PointValue = 500;
            base.AnimationFrameCount = 6;
            base.AnimationFramesPerSecond = 15;
            base.AnimationStartName = "alien1";
        }
    }
}
