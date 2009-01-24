using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyFirstGame.LevelObject
{
    interface IPattern
    {
        Vector2 UpdatePattern();
    }
}