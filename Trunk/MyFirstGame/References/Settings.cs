using Microsoft.Xna.Framework;

namespace MyFirstGame.References
{
    public sealed class Settings
    {
        private static Settings _settings = null;

        public Vector2 ScreenSize { get; set; }
        public float AspectRatio { get; set; }
        public GameTime GameTime { get; set; }

        public Vector2 TopLeft
        {
            get
            {
                return new Vector2(0, 0);
            }
        }

        public Vector2 TopRight
        {
            get
            {
                return new Vector2(ScreenSize.X, 0);
            }
        }

        public Vector2 BottomLeft
        {
            get
            {
                return new Vector2(0, ScreenSize.Y);
            }
        }

        public Vector2 BottomRight
        {
            get
            {
                return new Vector2(ScreenSize.X, ScreenSize.Y);
            }
        }

        public Vector2 OneThirdLeft
        {
            get
            {
                return new Vector2(0, ScreenSize.Y * 0.33f);
            }
        }

        public Vector2 MidLeft
        {
            get
            {
                return new Vector2(0, ScreenSize.Y * 0.5f);
            }
        }

        public Vector2 TwoThirdsLeft
        {
            get
            {
                return new Vector2(0, ScreenSize.Y + 0.66f);
            }
        }

        public Vector2 OneThirdRight
        {
            get
            {
                return new Vector2(ScreenSize.X, ScreenSize.Y * 0.33f);
            }
        }

        public Vector2 MidRight
        {
            get
            {
                return new Vector2(ScreenSize.X, ScreenSize.Y * 0.5f);
            }
        }

        public Vector2 TwoThirdsRight
        {
            get
            {
                return new Vector2(ScreenSize.X, ScreenSize.Y + 0.66f);
            }
        }

        public Vector2 OneThirdTop
        {
            get
            {
                return new Vector2(ScreenSize.X * 0.33f, 0);
            }
        }

        public Vector2 MidTop
        {
            get
            {
                return new Vector2(ScreenSize.X * 0.5f, 0);
            }
        }

        public Vector2 TwoThirdsTop
        {
            get
            {
                return new Vector2(ScreenSize.X * 0.66f, 0);
            }
        }

        public Vector2 OneThirdBottom
        {
            get
            {
                return new Vector2(ScreenSize.X * 0.33f, ScreenSize.Y);
            }
        }

        public Vector2 MidBottom
        {
            get
            {
                return new Vector2(ScreenSize.X * 0.5f, ScreenSize.Y);
            }
        }

        public Vector2 TwoThirdsBottom
        {
            get
            {
                return new Vector2(ScreenSize.X * 0.66f, ScreenSize.Y);
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
