using System.Text.Json.Serialization;

namespace MovieMagic.Models {
    public class SearchResult {
        [JsonPropertyName("Title")]
        public string Name { get; set; }
        [JsonPropertyName("imdbID")]
        public string ImdbId { get; set; }
        public string Year { get; set; }

        [JsonPropertyName("Poster")]
        public string PosterUrl { get; set; }
    }
}