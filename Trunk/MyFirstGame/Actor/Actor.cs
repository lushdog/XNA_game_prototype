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
        private string _tag;
        private Vector2 _position;
        private float _rotation;
        
        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }

        }

        public abstract Vector2 Origin { get; set; }
        
        public float Rotation
        {
            get
            {
                return _rotation;
            }

            set
            {
                _rotation = value;
            }

        }

        public Actor()
        {
            
        }

        public void MoveTo(int x, int y)
        {
            this.Position = new Vector2(x, y);
        }

        public void MoveRelative(int x, int y)
        {
            this.Position += new Vector2(x, y);
        }
    }
}
