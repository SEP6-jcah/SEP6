using System.Collections.Generic;

namespace Sep6Client.Model
{
    public class MovieList
    {
        public IList<Movie> Movies { get; set; }
        public int NrOfPages { get; set; }
        public int NrOfResults { get; set; }
    }
}