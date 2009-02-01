using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstGame.Utilities
{
    static class UtilityMethods
    {
        public static T NumToEnum<T>(int number)
        {
            return (T)Enum.ToObject(typeof(T), number);
        }
    }
}
