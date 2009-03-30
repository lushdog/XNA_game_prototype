using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyFirstGame.GameObject;
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
                return Settings.Instance.GameTime.TotalGameTime.TotalSeconds - StartTimeInSeconds;
            }
        }
        
        public int CurrentWave { get; set; }
        
        public bool IsStarted { get; private set; }
        
        public bool IsEnded { get; private set; }

        public Wave(int waveLengthInSeconds)
        {
            WaveLengthInSeconds = waveLengthInSeconds;
            IsStarted = false;
            IsEnded = false;
        }

        public void StartWave()
        {
            StartTimeInSeconds = Settings.Instance.GameTime.TotalGameTime.TotalSeconds;
            CurrentWave = 0;
            IsStarted = true;
        }

        public virtual void UpdateWave()
        {
            if (WaveElapsedTimeInSeconds > WaveLengthInSeconds)
            {
                EndWave();
            }

            //TODO: inactive targets are still moving...
            foreach (Target target in Targets)
            {
                target.MoveTo(target.Pattern.UpdatePattern());
            }
        }
        
        public void EndWave()
        {
            IsEnded = true;
            foreach (Target target in Targets)
            {
                target.MoveTo(new Vector2(-100, -100));
                target.IsActive = false;
            }
        }

    }
}
