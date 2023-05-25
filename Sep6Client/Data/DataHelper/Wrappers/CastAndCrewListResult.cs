using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sep6Client.Data.DataHelper.Wrappers
{
    public class CastAndCrewListResult
    {
        [Required]
        [JsonPropertyName("cast")]
        public IList<ActorResult> Actors { get; set; }
        
        [Required]
        [JsonPropertyName("crew")]
        public IList<CrewMemberResult> Crew { get; set; }
    }
}