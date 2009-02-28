﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyFirstGame.GameObject;

namespace MyFirstGame.LevelObject
{
    class FirstLevel : Level
    {
        public FirstLevel()
        {
            Tag = "This is the first level.";
            BackgroundSpriteSheetName = "background";

            Sprites = new List<Sprite>();
            FirstBackgroundSprite fs = new FirstBackgroundSprite();
            fs.Position = new Vector2(100, 200);
            Sprites.Add(fs);
                       
            Waves = new List<Wave>();
            FirstWave fw = new FirstWave();
            fw.Tag = "Wave1";
            Waves.Add(fw);

        }

        public override void UpdateLevel()
        {
            base.UpdateLevel();
        }
    }
}
