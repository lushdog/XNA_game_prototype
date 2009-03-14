
namespace MyFirstGame.GameObject
{
    class BearTarget : Target
    {
        public BearTarget()
        {
            base.PointValue = 500;
            base.AnimationFrameCount = 1;
            base.AnimationFramesPerSecond = 1;
            base.AnimationStartName = "bear";
            base.Scale = 1.0f;
        }
    }
}
