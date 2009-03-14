using System.Collections.Generic;
using MyFirstGame.GameObject;
using MyFirstGame.References;

namespace MyFirstGame.LevelObject
{
    class TestWave : Wave
    {
        public TestWave()
        {
            WaveLengthInSeconds = 15;
            base.Targets = new List<Target>();

            CatTarget t = new CatTarget();
            t.Position = Settings.Instance.OneThirdBottom;
            t.Pattern = new TestPattern(t.Position, 30f);
            t.IsActive = true;
            base.Targets.Add(t);

            BearTarget t1 = new BearTarget();
            t1.Position = Settings.Instance.BottomLeft;
            t1.Pattern = new TestPattern(t1.Position, 20f);
            t1.IsActive = true;
            base.Targets.Add(t1);

            SquidTarget t2 = new SquidTarget();
            t2.Position = Settings.Instance.MidRight;
            t2.Pattern = new TestPattern(t2.Position, 5f);
            t2.IsActive = true;
            base.Targets.Add(t2);

            ZombieTarget t3 = new ZombieTarget();
            t3.Position = Settings.Instance.OneThirdLeft;
            t3.Pattern = new TestPattern(t3.Position, 60f);
            t3.IsActive = true;
            base.Targets.Add(t3);



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
