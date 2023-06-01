using System;
using System.Collections.Generic;
using Sep6Client.Data.DataHelper.Wrappers;
using Sep6Client.Model;

namespace Sep6Client.Data.DataHelper.Mappers
{
    public static class MovieMapper
    {
        public static Movie ToMovie(MovieResult result)
        {
            try
            {
                return new Movie
                {
                    Id = result.Id,
                    Title = result.Title,
                    Description = result.Description,
                    Poster = $"http://image.tmdb.org/t/p/w500/{result.Poster}",
                    Rating = Math.Round(result.Rating, 2),
                    Votes = result.Votes,
                    ReleaseDate = result.ReleaseDate,
                    Language = result.Language,
                    Actors = new List<Actor>(),
                    Directors = new List<CrewMember>(),
                    Crew = new List<CrewMember>()
                };
            }
            catch (Exception e)
            {
                throw new Exception($"Error mapping result to movie: {e.Message}\n{e.StackTrace}");
            }
        }
    }
}