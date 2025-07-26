using Newtonsoft.Json;

namespace web_app.Models
{
    public class RawSearchResponse
    {
        [JsonProperty("res")]
        public SearchResponse Res { get; set; }
    }

    public class SearchResponse
    {
        [JsonProperty("results")]
        public List<SearchResultItem> Results { get; set; }
    }

    public class SearchResultItem
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("row")]
        public double Row { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public class IndexingRes 
    {
        //[JsonProperty("Result")]
        public bool Result { get; set; }
    }

}