using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SpriteSheetRuntime;

namespace MyFirstGame.References
{
    public sealed class Textures
    {
        private static Textures _instance = null;
       
        public SpriteSheet SpriteSheet { get; set; }

        public static Textures Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Textures();
                }
                return _instance;
            }            
        }

        private Textures()
        {
            
        }        
    }
}
