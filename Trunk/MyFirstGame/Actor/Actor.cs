using Microsoft.Xna.Framework;

namespace MyFirstGame
{
    abstract class Actor
    {
        public string Tag { get; set; }
        public Vector2 Position { get; set; }
        public abstract Vector2 Origin { get; }
        public float Rotation { get; set; }
        
        protected Actor()
        {
            
        }

        public void MoveTo(Vector2 position)
        {
            this.Position = position;
        }

        public void MoveRelative(Vector2 position)
        {
            this.Position += position;
        }
    }
}
