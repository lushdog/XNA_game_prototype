using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace MyFirstGame
{
    class TestLevel : Level
    {
        public TestLevel()
        {
            Tag = "This is the first level.";
            BackgroundSpriteSheetName = "background";

            Sprites = new List<Sprite>();
            TestBackgroundSprite fs = new TestBackgroundSprite();
            fs.Position = Settings.Instance.ScreenCenter;
            Sprites.Add(fs);
                       
            Waves = new List<Wave>();
            TestWave fw = new TestWave();
            fw.Tag = "Wave1";
            Waves.Add(fw);

        }

        public override void UpdateLevel()
        {
            base.UpdateLevel();
        }
    }
}
