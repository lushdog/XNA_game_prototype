using System;
using Microsoft.Xna.Framework;

namespace MyFirstGame
{
    static class UtilityMethods
    {
        public static T NumToEnum<T>(int number)
        {
            return (T)Enum.ToObject(typeof(T), number);
        }

        public static float RandomBetween(float min, float max)
        {
            return min + (float)new Random().NextDouble() * (max - min);
        }

        /// <summary>
        /// PickRandomDirection is used by InitializeParticles to decide which direction
        /// particles will move. The default implementation is a random vector in a
        /// circular pattern.
        /// </summary>
        public static Vector2 PickRandomDirection()
        {
            float angle = UtilityMethods.RandomBetween(0, MathHelper.TwoPi);
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }        
    }
}
