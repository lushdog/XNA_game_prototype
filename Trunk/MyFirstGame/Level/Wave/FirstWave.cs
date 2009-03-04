using System.Collections.Generic;
using MyFirstGame.GameObject;
using MyFirstGame.References;

namespace MyFirstGame.LevelObject
{
    class FirstWave : Wave
    {
        public FirstWave()
        {
            WaveLengthInSeconds = 15;
            base.Targets = new List<Target>();

            TestTarget t = new TestTarget();
            t.Position = Settings.Instance.OneThirdBottom;
            t.Pattern = new TestPattern(t.Position, 10f);
            t.IsActive = true;
            base.Targets.Add(t);

            TestTarget t1 = new TestTarget();
            t1.Position = Settings.Instance.BottomLeft;
            t1.Pattern = new TestPattern(t1.Position, 10f);
            t1.IsActive = true;
            base.Targets.Add(t1);

            TestTarget t2 = new TestTarget();
            t2.Position = Settings.Instance.MidRight;
            t2.Pattern = new TestPattern(t2.Position, 10f);
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
