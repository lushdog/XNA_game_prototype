using Microsoft.Xna.Framework;

namespace MyFirstGame.GameObject
{
    class TestBackgroundSprite : BackgroundSprite
    {
        public TestBackgroundSprite()
        {
            base.AnimationStartName = "testimage1";
            base.AnimationFrameCount = 5;
            base.AnimationFramesPerSecond = 1;
            base.Scale = 3.0f;
        }
    }
}
