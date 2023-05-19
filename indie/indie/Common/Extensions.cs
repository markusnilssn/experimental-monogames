using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndieGame.Common
{
    public static class CollectionExtensions
    {

        public static bool IsEmpty<T>(this T[] array)
        {
            return array.Length == 0;
        }

        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }
    }
}
