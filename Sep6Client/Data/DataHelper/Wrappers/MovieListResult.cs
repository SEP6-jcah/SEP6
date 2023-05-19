using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sep6Client.Data.DataHelper.Wrappers
{
    public class MovieListResult
    {
        [JsonPropertyName("results")]
        public IList<MovieResult> Movies { get; set; }
    }
}