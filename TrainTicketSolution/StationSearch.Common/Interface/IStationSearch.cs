using StationSearch.Common.DTO;

namespace StationSearch.Common.Interface
{
    public interface IStationSearch
    {
        SearchResult Search(string word);
    }
}
