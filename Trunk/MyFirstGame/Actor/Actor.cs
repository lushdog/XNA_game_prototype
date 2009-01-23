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
    public abstract class Actor
    {
        public string Tag { get; set; }
        public Vector2 Position { get; set; }
        public abstract Vector2 Origin { get; set; }
        public float Rotation { get; set; }

        public Actor()
        {
            
        }

        public void MoveTo(float x, float y)
        {
            this.Position = new Vector2(x, y);
        }

        public void MoveRelative(int x, int y)
        {
            this.Position += new Vector2(x, y);
        }
    }
}
