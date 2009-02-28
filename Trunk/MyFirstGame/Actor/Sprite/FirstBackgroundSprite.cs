using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstGame.GameObject
{
    class FirstBackgroundSprite : Sprite
    {
        public FirstBackgroundSprite()
        {
            base.AnimationStartName = "testimage";
            base.AnimationFrameCount = 1;
            base.AnimationFramesPerSecond = 0;
        }
    }
}
