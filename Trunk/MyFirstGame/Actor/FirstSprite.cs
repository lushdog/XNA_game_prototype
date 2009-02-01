using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstGame.GameObject
{
    class FirstSprite : Sprite
    {
        public FirstSprite()
        {
            SpriteTexture = References.Textures.Instance.FirstLevelSprite;            
        }
    }
}
