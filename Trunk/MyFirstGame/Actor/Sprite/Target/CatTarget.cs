
namespace MyFirstGame.GameObject
{
    class CatTarget : Target
    {
        public CatTarget()
        {
            base.PointValue = 500;
            base.AnimationFrameCount = 1;
            base.AnimationFramesPerSecond = 1;
            base.AnimationStartName = "cat";
            base.Scale = 1.0f;
        }
    }
}
