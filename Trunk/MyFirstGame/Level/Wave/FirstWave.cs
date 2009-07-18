using System.Collections.Generic;

namespace MyFirstGame
{
    class TestWave : Wave
    {
        public TestWave() : base(15)
        {
            base.Targets = new List<Target>();

            CatTarget t = new CatTarget();
            t.Position = Settings.Instance.GetScreenGridLocation(1, 1);
            t.Pattern = new TestPattern(t.Position, 20f);
            t.IsActive = true;
            base.Targets.Add(t);

            BearTarget t1 = new BearTarget();
            t1.Position = Settings.Instance.GetScreenGridLocation(2, 1);
            t1.Pattern = new TestPattern(t1.Position, 90f);
            t1.IsActive = true;
            base.Targets.Add(t1);

            SquidTarget t2 = new SquidTarget();
            t2.Position = Settings.Instance.GetScreenGridLocation(3, 1);
            t2.Pattern = new TestPattern(t2.Position, 40f);
            t2.IsActive = true;
            base.Targets.Add(t2);

            ZombieTarget t4 = new ZombieTarget();
            t4.Position = Settings.Instance.GetScreenGridLocation(4, 1);
            t4.Pattern = new TestPattern(t4.Position, 15f);
            t4.IsActive = true;
            base.Targets.Add(t4);

            BearTarget t5 = new BearTarget();
            t5.Position = Settings.Instance.GetScreenGridLocation(7, 1);
            t5.Pattern = new TestPattern(t5.Position, 1f);
            t5.IsActive = true;
            base.Targets.Add(t5);

            CatTarget t6 = new CatTarget();
            t6.Position = Settings.Instance.GetScreenGridLocation(6, 1);
            t6.Pattern = new TestPattern(t6.Position, 10f);
            t6.IsActive = true;
            base.Targets.Add(t6);

            SquidTarget t7 = new SquidTarget();
            t7.Position = Settings.Instance.GetScreenGridLocation(7, 7);
            t7.Pattern = new TestPattern(t7.Position, 20f);
            t7.IsActive = true;
            base.Targets.Add(t7);

        }        
    }
}
