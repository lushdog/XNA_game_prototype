
namespace MyFirstGame.GameObject
{
    class ZombieTarget : Target
    {
        public ZombieTarget()
        {
            base.PointValue = 500;
            base.AnimationFrameCount = 1;
            base.AnimationFramesPerSecond = 1;
            base.AnimationStartName = "zombie";
            base.Scale = 1.0f;
        }
    }
}
