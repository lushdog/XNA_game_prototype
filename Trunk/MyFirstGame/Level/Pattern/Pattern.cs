using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyFirstGame.References;

namespace MyFirstGame.LevelObject
{
    public abstract class Pattern
    {
        public List<Vector2> ControlPoints { get; set; }
        public int CurrentPoint { get; set; }
        public float CurrentWeight { get; set; }
        public Vector2 LastPosition { get; set; }
        public float CurrentSpeed { get; set; }

        public Vector2 UpdatePattern()
        {
            float time = (float)Settings.Instance.GameTime.ElapsedGameTime.TotalSeconds;

            if (CurrentWeight >= 1) //reached next CP
            {
                if (CurrentPoint < ControlPoints.Count - 3)
                {
                    CurrentPoint += 1;
                    CurrentWeight = 0.0f;
                }
            }
            else
            {
                CurrentWeight += 0.01f * CurrentSpeed * time;
            }

            Vector2 newPos = Vector2.CatmullRom(ControlPoints[CurrentPoint - 1], ControlPoints[CurrentPoint],
                ControlPoints[CurrentPoint + 1], ControlPoints[CurrentPoint + 2], CurrentWeight);

            return newPos;
        }
    }
}