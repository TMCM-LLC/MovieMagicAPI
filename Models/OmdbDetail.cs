using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MovieMagic.Models {
    public class OmdbDetail {
        
        [JsonPropertyName("imdbID")]
        public string ImdbId {get;set;}
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released {get;set;}
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
        public IEnumerable<OmdbRating> Ratings { get;set; }
    }

    public class OmdbRating {
        public string Source {get; set;}
        public string Value {get;set;}
    }
}