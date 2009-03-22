using Microsoft.Xna.Framework;
using MyFirstGame.GameObject;

namespace MyFirstGame.GameObject
{
    abstract class BackgroundSprite : Sprite
    {
        //we want Position.X/Y to be the center of drawn sprite
        //as opposed to other sprites that the top/left is Position.X/Y
        public override Rectangle DrawRectangle
        {
            get
            {
                Rectangle drawRectangle = base.DrawRectangle;
                drawRectangle.X -= drawRectangle.Width / 2;
                drawRectangle.Y -= drawRectangle.Height / 2;
                return drawRectangle;
            }
        }

        public BackgroundSprite(int animationFrameCount, int animationFramesPerSecond, string animationStartName, float scale)
            : base(animationFrameCount, animationFramesPerSecond, animationStartName, scale)
        { }




    }

}
