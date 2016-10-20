using StationSearch.Common.Interface;
using System.Collections.Generic;
using System.Linq;
using StationSearch.Common.DTO;
using StationSearch.Data;

namespace StationSearch
{
    public class StationSearchClass : IStationSearch
    {
        private Tree _tree;

        public StationSearchClass(IEnumerable<string> stations)
        {
            var stationList = new List<string>(stations.Distinct());
            stationList.Sort();
            _tree = new Tree(stationList);
        }

        public SearchResult Search(string word)
        {
            return _tree.Search(word);
        }
    }
}
