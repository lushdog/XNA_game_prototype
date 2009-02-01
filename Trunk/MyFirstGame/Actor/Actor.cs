using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace MyFirstGame.GameObject
{
    abstract class Actor
    {
        public string Tag { get; set; }
        public Vector2 Position { get; set; }
        public abstract Vector2 Origin { get; }
        public float Rotation { get; set; }
        
        public Actor()
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
