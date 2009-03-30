using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyFirstGame.References;

namespace MyFirstGame.GameObject
{
    abstract class Sprite : Actor
    {
        public float Scale { get; set; }

        public virtual Rectangle DrawRectangle
        {
            get
            {
                Rectangle sourceRectangle = Textures.Instance.SpriteSheet.SourceRectangle(GetSpriteSheetIndex());
                sourceRectangle.Width = (int)((float)sourceRectangle.Width * Scale);
                sourceRectangle.Height = (int)((float)sourceRectangle.Height * Scale);
                return new Rectangle((int)Position.X, (int)Position.Y,
                    (int)(sourceRectangle.Width), (int)(sourceRectangle.Height));
            }
        }
        
        public Color SpriteColor { get; set; }
        
        public override Vector2 Origin
        {
            get
            {
                Rectangle sourceRectangle = Textures.Instance.SpriteSheet.SourceRectangle(GetSpriteSheetIndex());
                return new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
            }
        }

        public int AnimationFramesPerSecond { get; set; }
        
        public int AnimationFrameCount { get; set; }
        
        public string AnimationStartName { get; set; }

        public int GetSpriteSheetIndex()
        {
            int index = Textures.Instance.SpriteSheet.GetIndex(AnimationStartName);
            index += (int)(Settings.Instance.GameTime.TotalGameTime.TotalSeconds * AnimationFramesPerSecond) % AnimationFrameCount;
            return index;
        }

        protected Sprite(int animationFrameCount,int animationFramesPerSecond, string animationStartName, 
            float scale)
        {
            AnimationFrameCount = animationFrameCount;
            AnimationFramesPerSecond = animationFramesPerSecond;
            AnimationStartName = animationStartName;
            Scale = scale;
        }
    }
}
