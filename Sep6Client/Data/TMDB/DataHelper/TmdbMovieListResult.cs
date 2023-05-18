using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sep6Client.Data.TMDB.DataHelper
{
    public class TmdbMovieListResult
    {
        [JsonPropertyName("results")]
        public IList<TmdbMovieResult> Movies { get; set; }
    }
}