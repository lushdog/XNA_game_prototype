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
        private Vector2 origin;

        public Rectangle DrawRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y,
                    (int)(SpriteTexture.Width * Settings.Instance.AspectRatio), (int)(SpriteTexture.Height));
            }
        }
        public string SpritePath { get; set; }
        public Color SpriteColor { get; set; }
        public Texture2D SpriteTexture { get; set; }

        public override Vector2 Origin
        {
            get
            {
                return new Vector2(SpriteTexture.Width / 2, SpriteTexture.Height / 2);
            }
        }
    }
}
