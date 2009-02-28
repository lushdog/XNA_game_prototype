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
            WaveLengthInSeconds = 15;
            base.Targets = new List<Target>();

            AlienTarget t = new AlienTarget();
            t.Position = Settings.Instance.OneThirdBottom;
            t.Pattern = new FirstPattern(t.Position, 50f);
            t.IsActive = true;
            base.Targets.Add(t);

            AlienTarget t1 = new AlienTarget();
            t1.Position = Settings.Instance.BottomLeft;
            t1.Pattern = new FirstPattern(t1.Position, 50f);
            t1.IsActive = true;
            base.Targets.Add(t1);

            AlienTarget t2 = new AlienTarget();
            t2.Position = Settings.Instance.MidRight;
            t2.Pattern = new FirstPattern(t2.Position, 50f);
            t2.IsActive = true;
            base.Targets.Add(t2);

        }

        public override void UpdateWave()
        {
            base.UpdateWave();

            foreach (Target target in Targets)
            {
                target.MoveTo(target.Pattern.UpdatePattern());
            }
        }
    }
}
