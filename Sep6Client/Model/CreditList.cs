using System.Collections.Generic;

namespace Sep6Client.Model
{
    public class CreditList
    {
        public int MovieId { get; set; }
        public IList<ActorCredits>? ActorCredits { get; set; }
        public IList<CrewCredits>? CrewCredits { get; set; }
        public double ActorMovieRatingAverage { get; set; }
        public double CrewMovieRatingAverage { get; set; }
    }
}