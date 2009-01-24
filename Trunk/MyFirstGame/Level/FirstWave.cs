using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFirstGame.GameObject;
using Microsoft.Xna.Framework;
using MyFirstGame.References;
using MyFirstGame.LevelObject;

namespace MyFirstGame.LevelObject
{
    class FirstWave : Wave
    {
        public FirstWave()
        {
            Tag = "This is my first wave.";
            WaveLengthInSeconds = 10;
            Targets = new List<Target>();
            
            AlienTarget t = new AlienTarget();
            t.Position = Settings.Instance.UpperLeft;
            t.Pattern = new FirstPattern();
            t.IsActive = true;
            Targets.Add(t);

            AlienTarget t1 = new AlienTarget();
            t1.Position = Settings.Instance.MidTop;
            t1.Pattern = new SecondPattern();
            t1.IsActive = true;
            Targets.Add(t1);
        }

        public override void UpdateWave()
        {
            base.UpdateWave();

            foreach (Target target in Targets)
            {
                target.MoveRelative(target.Pattern.UpdatePattern());
            }
            
        }
    }
}
