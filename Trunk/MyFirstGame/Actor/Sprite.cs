using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyFirstGame.References;

namespace MyFirstGame.GameObject
{
    class Sprite : Actor
    {
        public Rectangle DrawRectangle
        {
            get
            {
                //TODO: this is where we'll implement scaling...
                Rectangle sourceRectangle = Textures.Instance.SpriteSheet.SourceRectangle(GetSpriteSheetIndex());
                return new Rectangle((int)Position.X, (int)Position.Y,
                    (int)(sourceRectangle.Width), (int)(sourceRectangle.Height));
            }
        }
        public Color SpriteColor { get; set; }
        public override Vector2 Origin
        {
            get
            {
                return new Vector2(DrawRectangle.Width / 2, DrawRectangle.Height / 2);
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
    }
}
