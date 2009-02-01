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
            WaveLengthInSeconds = 10;
            Targets = new List<Target>();
            
            AlienTarget t = new AlienTarget();
            t.Position = Settings.Instance.OneThirdBottom;
            t.Pattern = new FirstPattern(t.Position);
            t.IsActive = true;
            Targets.Add(t);

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
