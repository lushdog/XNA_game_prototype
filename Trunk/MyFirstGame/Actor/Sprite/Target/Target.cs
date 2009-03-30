using Microsoft.Xna.Framework;
using MyFirstGame.LevelObject;

namespace MyFirstGame.GameObject
{
    abstract class Target : Sprite
    {
        public bool IsActive { get; set; }

        public int PointValue { get; set; }
        
        public Rectangle BoundingBox
        {
            get
            {
                Rectangle drawRectangle =  base.DrawRectangle;
                return new Rectangle((int)(drawRectangle.X - (Origin.X * Scale)), (int)(drawRectangle.Y - (Origin.Y * Scale)), drawRectangle.Width, drawRectangle.Height);
            }
        }        

        public Pattern Pattern { get; set; }

        protected Target(int pointValue, int animationFrameCount, int animationFramesPerSecond, string animationStartName, float scale) 
            : base(animationFrameCount, animationFramesPerSecond, animationStartName, scale)
        {
            PointValue = pointValue;
        }
    }
}
