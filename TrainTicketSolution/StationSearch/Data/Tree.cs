using StationSearch.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StationSearch.Data
{
    internal class Tree
    {
        private TreeNode _root;
        private List<string> _values;

        public Tree(List<string> values)
        {
            _values = new List<string>(values);
            _root = new TreeNode
            {
                Character = '\0',
                MinIndex = int.MaxValue,
                MaxIndex = int.MinValue,
                Children = new Dictionary<char, TreeNode>()
            };
            for (int i = 0; i < _values.Count; ++i)
            {
                AddValue(i, _values[i]);
            }
            _root.SetIndices();
        }

        public SearchResult Search(string value)
        {
            var node = GetLastNodeForValue(value);
            if (node == null)
            {
                return SearchResult.Empty;
            }
            return GetSearchResultFromNode(node);
        }


        #region Private functions

        private SearchResult GetSearchResultFromNode(TreeNode node)
        {
            var items = GetItemsBetweenIndices(node.MinIndex, node.MaxIndex);
            var nexts = GetNextCharacters(node);

            return new SearchResult(items, nexts);
        }

        private string[] GetItemsBetweenIndices(int minIndex, int maxIndex)
        {
            return _values
                .Skip(minIndex)
                .Take(maxIndex - minIndex + 1)
                .ToArray();
        }

        private char[] GetNextCharacters(TreeNode node)
        {
            return node
                .Children
                .Keys
                .OrderBy(c => c)
                .ToArray();
        }

        private TreeNode GetLastNodeForValue(string value)
        {
            return TraverseTreeForValue(value, 
                (node, c) => false, 
                null, 
                node => node);
        }

        private void AddValue(int index, string value)
        {
            TraverseTreeForValue(value,
                (node, c) =>
                {
                    EnsureNodeChild(node, c);
                    return true;
                },
                node =>
                {
                    node.MinIndex = index;
                    node.MaxIndex = index;
                },
                node => node);
        }

        private void EnsureNodeChild(TreeNode node, char c)
        {
            node.Children[c] = new TreeNode
            {
                Character = c,
                MinIndex = int.MaxValue,
                MaxIndex = int.MinValue,
                Children = new Dictionary<char, TreeNode>()
            };
        }

        /// <summary>
        /// This is to traverse the tree according to the "value" parameter. Handlers are used to tap into the process.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The string to traverse the tree by</param>
        /// <param name="notFoundHandler">Handler for "current character not found"</param>
        /// <param name="endOfValueHandler">Handler for "we are at the node of the last character"</param>
        /// <param name="finishedHandler">Handler for "traverse done, create result"</param>
        /// <returns></returns>
        private T TraverseTreeForValue<T>(string value,
            Func<TreeNode, char, bool> notFoundHandler,
            Action<TreeNode> endOfValueHandler,
            Func<TreeNode, T> finishedHandler)
            where T : class
        {
            notFoundHandler = notFoundHandler ?? DefaultNotFoundHandler;
            finishedHandler = finishedHandler ?? DefaultFinishedHandler<T>;
            var valueLength = value.Length;
            var node = _root;
            for (int i = 0; i < valueLength; ++i)
            {
                var c = value[i];
                if (!node.Children.ContainsKey(c) && !notFoundHandler(node, c))
                {
                    return null;
                }
                node = node.Children[c];

                if (endOfValueHandler != null && i == valueLength - 1)
                {
                    endOfValueHandler(node);
                }
            }
            return finishedHandler(node);
        }

        private Func<TreeNode, char, bool> DefaultNotFoundHandler = (node, c) => true;

        private T DefaultFinishedHandler<T>(TreeNode node)
            where T : class
        {
            return null;
        }

        #endregion
    }
}
