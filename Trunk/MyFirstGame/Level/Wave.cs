using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFirstGame.GameObject;
using Microsoft.Xna.Framework;

namespace MyFirstGame.LevelObject
{
    abstract class Wave
    {
        private GameTime _gameTime; 
        
        public string Tag { get; set; }
        public List<Target> Targets { get; set; }
        public GameTime GameTime 
        {
            get
            {
                return _gameTime;
            }
            set
            {
                _gameTime = value;
            }
        }
        public double StartTimeInSeconds { get; set; }
        public double WaveLengthInSeconds { get; set; }
        public double ElapsedTimeInSeconds
        {
            get
            {
                return GameTime.TotalRealTime.TotalSeconds - StartTimeInSeconds;
            }
        }
        public int CurrentWave { get; set; }
        public bool IsStarted { get; private set; }
        public bool IsEnded { get; private set; }

        public Wave(double waveLengthInSeconds)
        {
            IsStarted = false;
            IsEnded = false;
            WaveLengthInSeconds = waveLengthInSeconds;
        }

        public void StartWave(ref GameTime gameTime)
        {
            GameTime = gameTime;
            StartTimeInSeconds = GameTime.TotalRealTime.TotalSeconds;
            CurrentWave = 0;
            IsStarted = true;
        }

        public virtual void UpdateWave()
        {
            if (ElapsedTimeInSeconds > WaveLengthInSeconds)
            {
                EndWave();
            }
        }
        
        public void EndWave()
        {
            IsEnded = true;
            foreach (Target target in Targets)
            {
                //TODO:move target offscreen and make inactive
            }
        }

    }
}
