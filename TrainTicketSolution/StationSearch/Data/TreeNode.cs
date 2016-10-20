using System;
using System.Collections.Generic;

namespace StationSearch.Data
{
    internal class TreeNode
    {
        public char Character;
        public int MinIndex;
        public int MaxIndex;
        public Dictionary<char, TreeNode> Children;

        /// <summary>
        /// Set current Node's min and max index values according to the index values of the children. Recursive
        /// </summary>
        /// <returns>Tuple of min and max index values for all the children</returns>
        public Tuple<int, int> SetIndices()
        {
            int minIndex = int.MaxValue;
            int maxIndex = int.MinValue;

            foreach (var kvp in Children)
            {
                var pair = kvp.Value.SetIndices();
                if (pair.Item1 < minIndex) minIndex = pair.Item1;
                if (pair.Item2 > maxIndex) maxIndex = pair.Item2;
            }
            if (minIndex < MinIndex) MinIndex = minIndex;
            if (maxIndex > MaxIndex) MaxIndex = maxIndex;

            return new Tuple<int, int>(MinIndex, MaxIndex);
        }
    }
}
