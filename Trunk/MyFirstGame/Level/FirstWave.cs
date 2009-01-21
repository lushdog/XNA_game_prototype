using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFirstGame.GameObject;
using Microsoft.Xna.Framework;
using MyFirstGame.References;

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
            t.Position = new Vector2(300, 300);
            t.AIActorStates.Add(AIActorState.Active);
            Targets.Add(t);
        }

        public override void UpdateWave()
        {
            base.UpdateWave();

            //TODO: refactor this out as this is basis for movement everwhere
            //TODO: use this method of movement for Player
            foreach (Target target in Targets)
            {
                float time = (float)Settings.Instance.GameTime.ElapsedGameTime.TotalSeconds;
                int direction = 1;
                float speed = 5.0f;
                float distance = time * direction * speed;

                target.Position = new Vector2(target.Position.X + distance,
                    target.Position.Y + distance);
            }
            
        }
    }
}
