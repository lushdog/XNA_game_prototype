
namespace MyFirstGame.GameObject
{
    class SquidTarget : Target
    {
        public SquidTarget()
        {
            base.PointValue = 500;
            base.AnimationFrameCount = 1;
            base.AnimationFramesPerSecond = 1;
            base.AnimationStartName = "squid";
            base.Scale = 1.0f;
        }
    }
}
