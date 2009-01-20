using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFirstGame.GameObject;
using Microsoft.Xna.Framework;
using MyFirstGame.LevelObject;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame.LevelObject
{
    abstract class Level
    {
        private GameTime _gameTime; 
        
        public string Tag { get; set; }
        public int LevelNumber { get; set; }
        public List<Wave> Waves { get; set; }
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
        public double LevelLengthInSeconds { get; set; }
        public double ElapsedTimeInSeconds
        {
            get
            {
                return GameTime.TotalRealTime.TotalSeconds - StartTimeInSeconds;
            }
        }
        public int CurrentWave { get; set; }
        public Texture2D Background { get; set; }
        public bool IsStarted { get; private set; }
        public bool IsEnded { get; private set; }

        public Level(double levelLengthInSeconds)
        {
            LevelLengthInSeconds = levelLengthInSeconds;
            IsStarted = false;
            IsEnded = false;
        }

        public void StartLevel(ref GameTime gameTime)
        {
            GameTime = gameTime;
            StartTimeInSeconds = GameTime.TotalRealTime.TotalSeconds;
            CurrentWave = 0;
            IsStarted = true;
        }

        public virtual void UpdateLevel()
        {
            if (CurrentWave < Waves.Count)
            {
                //Wave.cs handles this now
                //if (ElapsedTimeInSeconds > Waves[CurrentWave].WaveLengthInSeconds)
                //{
                //    Waves[CurrentWave].EndWave();
                //    CurrentWave += 1;                    
                //} 

                if (!Waves[CurrentWave].IsEnded)
                {
                    //TODO: when level ends this here throws exception 
                    if ((ElapsedTimeInSeconds >= Waves[CurrentWave].StartTimeInSeconds)
                        && (ElapsedTimeInSeconds <= Waves[CurrentWave].WaveLengthInSeconds))
                    {
                        if (Waves[CurrentWave].IsStarted)
                            Waves[CurrentWave].UpdateWave();
                        else
                            Waves[CurrentWave].StartWave(ref _gameTime);
                    }
                }
                else
                {
                    CurrentWave += 1;
                }
            }
            if (ElapsedTimeInSeconds > LevelLengthInSeconds)
            {
                EndLevel();
            }
        }

        public void EndLevel()
        {
            IsEnded = true;
            foreach (Wave wave in Waves)
            {
                //TODO:clean up waves
            }
        }

    }
}
