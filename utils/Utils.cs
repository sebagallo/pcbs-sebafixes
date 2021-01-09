using System;

namespace SebaFixes.utils
{
    public class Utils
    {
        public static T[] fixArray<T>(T input)
        {
            return new T[] {input};
        }
    }
}