using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_11_Session13_Depi
{
    internal class AnagramEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y)
        {
            if (x == null || y == null) return x == y;
            return GetSortedKey(x) == GetSortedKey(y);
        }

        public int GetHashCode(string obj)
        {
            return GetSortedKey(obj).GetHashCode();
        }

        private static string GetSortedKey(string s)
        {
            var chars = s.Trim().ToLowerInvariant().ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }
    }
}
