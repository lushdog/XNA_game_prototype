using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyFirstGame.References;

namespace MyFirstGame.GameObject
{
    class AlienTarget : Target
    {
        public AlienTarget(Vector2 maxPosition) : base(maxPosition)
        {
            base.SpritePath = "sprites\\alien";
            base.Sprite = Textures.Instance.AlienTexture;
            base.PointValue = 500;
        }
    }
}
