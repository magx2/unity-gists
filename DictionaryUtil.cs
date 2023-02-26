using System.Collections.Generic;

namespace Misc
{
    public static class DictionaryUtil
    {
        public static IDictionary<TKey, TValue> Sum<TKey, TValue>(
            IDictionary<TKey, TValue> first,
            IDictionary<TKey, TValue> second) {
            first ??= new Dictionary<TKey, TValue>();
            second ??= new Dictionary<TKey, TValue>();
            var dict = new Dictionary<TKey, TValue>(first.Count + second.Count);
            foreach (var entry in first) dict[entry.Key] = entry.Value;
            foreach (var entry in second) dict[entry.Key] = entry.Value;
            return dict;
        }
    }
}