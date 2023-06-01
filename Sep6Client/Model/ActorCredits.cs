namespace Sep6Client.Model
{
    public class ActorCredits
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public string? MovieReleaseDate { get; set; }
        public double MovieRating { get; set; }
        public int Votes { get; set; }
        public string? Character { get; set; }
    }
}