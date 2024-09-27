using System;
using System.Collections.Generic;

namespace Image_Utility.Extensions
{
    public static class ListExtensions
    {
        private static Random rng = new();
        public static void Shuffle<T>(this IList<T> items)
        {
            int n = items.Count;
            while(n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = items[k];
                items[k] = items[n];
                items[n] = value;
            }
        }
    }
}
