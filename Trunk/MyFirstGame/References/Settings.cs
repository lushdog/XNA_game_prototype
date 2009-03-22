using Microsoft.Xna.Framework;
using System;

namespace MyFirstGame.References
{
    public sealed class Settings
    {
        private static Settings _settings = null;

        public Vector2 ScreenSize { get; set; }
        public float AspectRatio { get; set; }
        public GameTime GameTime { get; set; }
        
        //Screen location grid
        public const int numGridPointsHorizontal = 7;
        public const int numGridPointsVertical = 7;        
        
        /// <summary>
        /// Returns a point on screen based on given inputs using a grid based system, (0,0) being upper left, (7,7) being lower right.
        /// </summary>
        /// <param name="indexHorizontal">Horizontal index of point, valid values 0-7.</param>
        /// <param name="indexVertical">Vertical index of point, valid values 0-7.</param>
        /// <returns>Location of point requested.</returns>
        public Vector2 GetScreenGridLocation(int indexHorizontal, int indexVertical)
        {
            if (indexHorizontal < 0 || indexHorizontal > numGridPointsHorizontal)
            {
                throw new Exception("indexHorizontal must be greater than zero and less than numGridPointsHorizontal.");
            }

            if (indexVertical < 0 || indexVertical > numGridPointsVertical)
            {
                throw new Exception("indexVertical must be greater than zero and less than numGridPointsVertical.");
            }
            
            return new Vector2(ScreenSize.X * indexHorizontal / numGridPointsHorizontal,
                ScreenSize.Y * indexVertical / numGridPointsVertical);
        }

        public Vector2 ScreenTopRight
        {
            get
            {
                return new Vector2(ScreenSize.X, 0);
            }
        }

        public Vector2 ScreenBottomLeft
        {
            get
            {
                return new Vector2(0, ScreenSize.Y);
            }
        }

        public Vector2 ScreenBottomRight
        {
            get
            {
                return new Vector2(ScreenSize.X, ScreenSize.Y);
            }
        }

        public Vector2 ScreenOneThirdLeft
        {
            get
            {
                return new Vector2(0, ScreenSize.Y * 0.33f);
            }
        }

        public Vector2 ScreenMidLeft
        {
            get
            {
                return new Vector2(0, ScreenSize.Y * 0.5f);
            }
        }

        public Vector2 ScreenTwoThirdsLeft
        {
            get
            {
                return new Vector2(0, ScreenSize.Y + 0.66f);
            }
        }

        public Vector2 ScreenOneThirdRight
        {
            get
            {
                return new Vector2(ScreenSize.X, ScreenSize.Y * 0.33f);
            }
        }

        public Vector2 ScreenMidRight
        {
            get
            {
                return new Vector2(ScreenSize.X, ScreenSize.Y * 0.5f);
            }
        }

        public Vector2 ScreenTwoThirdsRight
        {
            get
            {
                return new Vector2(ScreenSize.X, ScreenSize.Y + 0.66f);
            }
        }

        public Vector2 ScreenOneThirdTop
        {
            get
            {
                return new Vector2(ScreenSize.X * 0.33f, 0);
            }
        }

        public Vector2 ScreenMidTop
        {
            get
            {
                return new Vector2(ScreenSize.X * 0.5f, 0);
            }
        }

        public Vector2 ScreenTwoThirdsTop
        {
            get
            {
                return new Vector2(ScreenSize.X * 0.66f, 0);
            }
        }

        public Vector2 ScreenOneThirdBottom
        {
            get
            {
                return new Vector2(ScreenSize.X * 0.33f, ScreenSize.Y);
            }
        }

        public Vector2 ScreenMidBottom
        {
            get
            {
                return new Vector2(ScreenSize.X * 0.5f, ScreenSize.Y);
            }
        }

        public Vector2 ScreenTwoThirdsBottom
        {
            get
            {
                return new Vector2(ScreenSize.X * 0.66f, ScreenSize.Y);
            }
        }

        public Vector2 ScreenCenter
        {
            get
            {
                return new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2);
            }
        }


        public static Settings Instance 
        {
            get
            {
                if (_settings == null)
                {
                    _settings = new Settings();
                }
                return _settings;
            }            
        }

        private Settings()
        {
            
        }        
    }
}
