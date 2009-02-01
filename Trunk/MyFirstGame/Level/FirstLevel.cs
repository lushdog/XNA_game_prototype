using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstGame.LevelObject
{
    class FirstLevel : Level
    {
        public FirstLevel()
        {
            Tag = "This is the first level.";
            Background = References.Textures.Instance.FirstLevelBackground;
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
