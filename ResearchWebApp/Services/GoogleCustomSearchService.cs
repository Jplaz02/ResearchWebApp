namespace ResearchWebApp.Services
{
    public class GoogleCustomSearchService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "";
        private const string SearchEngineId = "";

        public GoogleCustomSearchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Modified SearchAsync method to accept a result limit
        public async Task<List<SearchResult>> SearchAsync(string query, int resultCount = 10)
        {
            var url = $"https://www.googleapis.com/customsearch/v1?q={Uri.EscapeDataString(query)}&key={ApiKey}&cx={SearchEngineId}&num={resultCount}";
            var response = await _httpClient.GetFromJsonAsync<GoogleSearchResponse>(url);

            return response?.Items?.Take(resultCount).Select(i => new SearchResult
            {
                Keyword = query,
                Title = i.Title,
                Preview = i.Snippet,
                Link = i.Link
            }).ToList() ?? new List<SearchResult>();
        }
    }

    public class GoogleSearchResponse
    {
        public List<SearchItem> Items { get; set; }
    }

    public class SearchItem
    {
        public string Title { get; set; }
        public string Snippet { get; set; }
        public string Link { get; set; }
    }

    public class SearchResult
    {
        public string Keyword { get; set; }
        public string Title { get; set; }
        public string Preview { get; set; }
        public string Link { get; set; }
    }
}
