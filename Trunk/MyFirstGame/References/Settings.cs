using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MyFirstGame.References
{
    public sealed class Settings
    {
        private static Settings _settings = null;

        public Vector2 ScreenSize { get; set; }
        public GameTime GameTime { get; set; }
        
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
