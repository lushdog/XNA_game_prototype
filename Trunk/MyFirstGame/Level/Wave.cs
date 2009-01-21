using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFirstGame.GameObject;
using Microsoft.Xna.Framework;
using MyFirstGame.References;

namespace MyFirstGame.LevelObject
{
    abstract class Wave
    {
        public string Tag { get; set; }
        public List<Target> Targets { get; set; }
        public double StartTimeInSeconds { get; set; }
        public double WaveLengthInSeconds { get; set; }
        public double WaveElapsedTimeInSeconds
        {
            get
            {
                return Settings.Instance.GameTime.TotalRealTime.TotalSeconds - StartTimeInSeconds;
            }
        }
        public int CurrentWave { get; set; }
        public bool IsStarted { get; private set; }
        public bool IsEnded { get; private set; }

        public Wave()
        {
            IsStarted = false;
            IsEnded = false;
        }

        public void StartWave()
        {
            StartTimeInSeconds = Settings.Instance.GameTime.TotalRealTime.TotalSeconds;
            CurrentWave = 0;
            IsStarted = true;
        }

        public virtual void UpdateWave()
        {
            if (WaveElapsedTimeInSeconds > WaveLengthInSeconds)
            {
                EndWave();
            }
        }
        
        public void EndWave()
        {
            IsEnded = true;
            foreach (Target target in Targets)
            {
                target.MoveTo(-100, -100);
                target.AIActorStates.Remove(AIActorState.Active);
            }
        }

    }
}
