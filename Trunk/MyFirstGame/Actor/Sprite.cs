using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyFirstGame.GameObject
{
	class Sprite
	{
		
        //MATT: if this guy inherits from Actor you get tag, position, rotation etc. etc.
        //Are AIActor and Player going to inherit from this guy?

		//JOE: 
		// I think Sprite might just be a reimagining of Actor.
		// What do you think about changing Actor to assume it's got a Texture2D?
		// If Actor doesn't assume a visible element, then why even have position and rotation and so on?
		// Otherwise, I don't think Sprite really fits in the hierarchy.  It could *belong* to
		// AIActor and Player and AlienTarget, etc, without inheriting to them, I suppose.

        private string _imagePath;
		private Texture2D _image;
		private float _rotation;
		private Vector2 _origin;
		private Vector2 _rotationOrigin;
		private int _x;
		private int _y;
		private int _width;
		private int _height;
		private float _scaleX;
		private float _scaleY;
		private float _alpha;

		public string ImagePath 
		{ 
			get
			{
				return _imagePath;
			} 
			set
			{
				_imagePath = value;
			} 
		}

		public Texture2D Image
		{
			get
			{
				return _image;
			}
			set
			{
				_image = value;
			}
		}

		public int X
		{
			get
			{
				return _x;
			}
			set
			{
				_x = value;
			}
		}

		public int Y
		{
			get
			{
				return _y;
			}
			set
			{
				_y = value;
			}
		}

		public Vector2 Location
		{
			get
			{
				return new Vector2(X, Y);
			}
			set
			{
				X = (int)value.X;
				Y = (int)value.Y;
			}
		}

		public Sprite()
		{
			X = 0;
			Y = 0;
		}
	}
}
