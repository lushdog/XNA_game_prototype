
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
