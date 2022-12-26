using System;
using System.Collections.Generic;

namespace Practics.GIBDD.VIN.Extensions
{
    public static class LinqExtensions
    {
        public static IList<(T1, T2)> ToTupledPairs<T1, T2>(this IList<T1> collection1, IList<T2> collection2)
        {
            var result = new List<(T1, T2)>();
            for (var i = 0; i < collection1.Count; i++)
            {
                result.Add((collection1[i], collection2[i]));
            }
            return result;
        }
    }
}