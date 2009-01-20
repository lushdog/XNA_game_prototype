using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstGame.LevelObject
{
    class FirstLevel : Level
    {
        public FirstLevel() : base(30)
        {
            Tag = "This is the first level.";
            Waves = new List<Wave>();
            Waves.Add(new FirstWave(20));
        }

        public override void UpdateLevel()
        {
            base.UpdateLevel();
        }
    }
}
