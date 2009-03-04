using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyFirstGame.LevelObject
{
    class TestPattern : Pattern
    {
        public TestPattern(Vector2 startPosition, float speed)
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
            CurrentSpeed = speed;
        }
    }
}
