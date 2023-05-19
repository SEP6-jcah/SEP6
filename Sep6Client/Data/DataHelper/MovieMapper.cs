using System;
using Sep6Client.Data.DataHelper.Wrappers;
using Sep6Client.Model;

namespace Sep6Client.Data.DataHelper
{
    public class MovieMapper
    {
        public static Movie ToTmdbMovie(MovieResult result)
        {
            try
            {
                return new Movie
                {
                    Id = result.Id,
                    Title = result.Title,
                    Description = result.Description,
                    Poster = result.Poster,
                    Rating = result.Rating,
                    Votes = result.Votes,
                    Actors = result.Actors,
                    ReleaseDate = result.ReleaseDate,
                    Language = result.Language
                };
            }
            catch (Exception e)
            {
                throw new Exception($"Error mapping result to movie: {e.Message}\n{e.StackTrace}");
            }
        }
    }
}