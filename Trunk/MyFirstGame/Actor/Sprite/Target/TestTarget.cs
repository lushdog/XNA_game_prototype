
namespace MyFirstGame.GameObject
{
    class TestTarget : Target
    {
        public TestTarget()
        {
            base.PointValue = 500;
            base.AnimationFrameCount = 6;
            base.AnimationFramesPerSecond = 15;
            base.AnimationStartName = "alien1";
            base.Scale = 2.0f;
        }
    }
}
