using System.Collections.Generic;

namespace MovieMagic.Models {
    public class OmdbSearchResult {
        public IEnumerable<SearchResult> Search { get; set; }
    }
}