using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyFirstGame.References;

namespace MyFirstGame.LevelObject
{
    class FirstPattern : IPattern
    {
        public List<Vector2> ControlPoints { get; set; }
        public int CurrentPoint { get; set; }
        public float CurrentWeight { get; set; }
        public Vector2 LastPosition { get; set; }

        public FirstPattern(Vector2 startPosition)
        {
            ControlPoints = new List<Vector2>();
            ControlPoints.Add(startPosition);  //First control point is never reached
            ControlPoints.Add(startPosition);
            ControlPoints.Add(new Vector2(75, 300));
            ControlPoints.Add(new Vector2(400, 200));
            ControlPoints.Add(new Vector2(400, 400));
            ControlPoints.Add(new Vector2(100, 100));
            ControlPoints.Add(new Vector2(500, 600));
            ControlPoints.Add(new Vector2(500, 600)); //Second last control point is never reached
            ControlPoints.Add(new Vector2(500, 600)); //Last control point is never reached 

            CurrentWeight = 0.0f;
            CurrentPoint = 1;
        }

        public Vector2 UpdatePattern()
        {
            float time = (float)Settings.Instance.GameTime.ElapsedGameTime.TotalSeconds;
            int speed = 100;           
            
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
                CurrentWeight += 0.01f * speed * time;
            }

            Vector2 newPos = Vector2.CatmullRom(ControlPoints[CurrentPoint - 1], ControlPoints[CurrentPoint],
                ControlPoints[CurrentPoint + 1], ControlPoints[CurrentPoint + 2], CurrentWeight);

            Console.WriteLine(newPos + ":" + CurrentPoint + ":" + CurrentWeight);
            return newPos;
        }
    }
}
