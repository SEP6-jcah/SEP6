namespace Sep6Client.Model
{
    public class CrewCredits
    {
        public int MovieId { get; set; }
        public string? MovieTitle { get; set; }
        public string? MovieReleaseDate { get; set; }
        public double MovieRating { get; set; }
        public int Votes { get; set; }
        public string? Job { get; set; }
        public string? Department { get; set; }
    }
}