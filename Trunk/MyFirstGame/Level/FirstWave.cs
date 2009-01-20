using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFirstGame.GameObject;
using Microsoft.Xna.Framework;

namespace MyFirstGame.LevelObject
{
    class FirstWave : Wave
    {
        public FirstWave(double waveLengthInSeconds) : base(waveLengthInSeconds)
        {
            Tag = "This is my first wave.";
            Targets = new List<Target>();
            AlienTarget t = new AlienTarget(new Vector2(800,600));
            t.Position = new Vector2(300, 300);
            t.AIActorStates.Add(AIActorState.Active);
            Targets.Add(t);
        }

        public override void UpdateWave()
        {
            base.UpdateWave();

            //TODO: check why this isn't working
            foreach (Target target in Targets)
            {
                target.Position = new Vector2(target.Position.X + (int)GameTime.ElapsedRealTime.TotalSeconds,
                    target.Position.Y + (int)GameTime.ElapsedRealTime.TotalSeconds);
            }
            
        }
    }
}
