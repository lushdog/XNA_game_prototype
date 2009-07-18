using System;

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
    }
}
