using System.Collections.Generic;

namespace Advent
{
    public static class EnumerableExtensions
    {
        public static TCollection AddRange<TCollection, TItem>(this TCollection collection, IEnumerable<TItem> items) where TCollection : ICollection<TItem>
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }

            return collection;
        }
    }
}
