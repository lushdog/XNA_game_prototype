using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyFirstGame
{
    class TestPattern : Pattern
    {
        public TestPattern(Vector2 startPosition, float speed) : base(startPosition, speed)
        {
            ControlPoints.Add(new Vector2(75, 300));
            ControlPoints.Add(new Vector2(400, 200));
            ControlPoints.Add(new Vector2(400, 400));
            ControlPoints.Add(new Vector2(100, 100));
            ControlPoints.Add(new Vector2(500, 600));
            ControlPoints.Add(new Vector2(500, 600)); 
        }
    }
}
