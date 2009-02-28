using System.Collections.Generic;
using MyFirstGame.GameObject;
using MyFirstGame.References;

namespace MyFirstGame.LevelObject
{
    abstract class Level
    {
        public string Tag { get; set; }
        public int LevelNumber { get; set; }
        public List<Wave> Waves { get; set; }
        public double StartTimeInSeconds { get; set; }
        public double LevelLengthInSeconds 
        {
            get
            {
                double length = 0;
                foreach (Wave wave in Waves)
                {
                    length += wave.WaveLengthInSeconds;
                }
                return length;
            }
        
        }
        public double LevelElapsedTimeInSeconds
        {
            get
            {
                return Settings.Instance.GameTime.TotalGameTime.TotalSeconds - StartTimeInSeconds;
            }
        }
        public int CurrentWaveIndex { get; set; }
        public string BackgroundSpriteSheetName { get; set; }
        public List<Sprite> Sprites { get; set; }
        public bool IsStarted { get; set; }
        public bool IsEnded { get; set; }

        public Level()
        {
            IsStarted = false;
            IsEnded = false;
        }

        public void StartLevel()
        {
            StartTimeInSeconds = Settings.Instance.GameTime.TotalGameTime.TotalSeconds;
            CurrentWaveIndex = 0;
            IsStarted = true;
        }

        public virtual void UpdateLevel()
        {            
            if (!Waves[CurrentWaveIndex].IsEnded)
            {
                //if ((ElapsedTimeInSeconds >= Waves[CurrentWave].StartTimeInSeconds)
                //    && (ElapsedTimeInSeconds <= Waves[CurrentWave].WaveLengthInSeconds))
                //{
                    if (Waves[CurrentWaveIndex].IsStarted)
                        Waves[CurrentWaveIndex].UpdateWave();
                    else
                        Waves[CurrentWaveIndex].StartWave();
                //}                        
            }
            else
            {
                CurrentWaveIndex += 1;
                if (CurrentWaveIndex == Waves.Count)
                {
                    EndLevel();
                }
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
