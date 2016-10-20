namespace StationSearch.Common.DTO
{
    public class SearchResult
    {
        public static readonly SearchResult Empty = new SearchResult(new string[0], new char[0]);

        public string[] Stations { get; }
        public char[] NextChars { get; }

        public SearchResult(string[] stations, char[] nextChars)
        {
            Stations = stations;
            NextChars = nextChars;
        }
    }
}
