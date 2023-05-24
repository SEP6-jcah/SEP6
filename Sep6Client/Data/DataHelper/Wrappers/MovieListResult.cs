using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sep6Client.Data.DataHelper.Wrappers
{
    public class MovieListResult
    {
        [JsonPropertyName("results")]
        public IList<MovieResult> Movies { get; set; }
        [JsonPropertyName("total_pages")]
        public int NrOfPages { get; set; }
        [JsonPropertyName("total_results")]
        public int NrOfMovies { get; set; }
    }
}