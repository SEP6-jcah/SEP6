using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sep6Client.Data.DataHelper.Wrappers
{
    public class PersonListResult
    {
        [JsonPropertyName("results")]
        public IList<PersonResult> Persons { get; set; }
    }
}