using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyFirstGame.References;

namespace MyFirstGame.LevelObject
{
    public abstract class Pattern
    {
        /// <summary>
        /// List of control points that patter will follow using Catmull-Rom interpolation.
        /// Notes: First and last two control points are not reached.
        /// </summary>
        public List<Vector2> ControlPoints { get; set; }

        public int CurrentControlPoint { get; set; }
        
        public float CurrentWeight { get; set; }
        
        public Vector2 LastPosition { get; set; }
        
        public float CurrentSpeed { get; set; }

        public Vector2 UpdatePattern()
        {
            float time = (float)Settings.Instance.GameTime.ElapsedGameTime.TotalSeconds;

            if (CurrentWeight >= 1) //reached next CP
            {
                if (CurrentControlPoint < ControlPoints.Count - 3)
                {
                    CurrentControlPoint += 1;
                    CurrentWeight = 0.0f;
                }
            }
            else
            {
                CurrentWeight += 0.01f * CurrentSpeed * time;
            }

            Vector2 newPos = Vector2.CatmullRom(ControlPoints[CurrentControlPoint - 1], ControlPoints[CurrentControlPoint],
                ControlPoints[CurrentControlPoint + 1], ControlPoints[CurrentControlPoint + 2], CurrentWeight);

            return newPos;
        }

        public Pattern(Vector2 startPosition, float speed)
        {
            ControlPoints = new List<Vector2>();
            ControlPoints.Add(startPosition);  
            ControlPoints.Add(startPosition);

            CurrentWeight = 0.0f;
            CurrentControlPoint = 1;
            CurrentSpeed = speed;
        }
    }
}