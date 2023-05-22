using System;
using System.Linq;
using Sep6Client.Data.DataHelper.Wrappers;
using Sep6Client.Model;

namespace Sep6Client.Data.DataHelper
{
    public static class MovieMapper
    {
        public static Movie ToMovie(MovieResult result)
        {
            try
            {
                // TODO: Link Persons to Movies - Aldís 22.05.23
                // var actors = result.Actors.Select(PersonMapper.ToPerson).ToList();
                return new Movie
                {
                    Id = result.Id,
                    Title = result.Title,
                    Description = result.Description,
                    Poster = result.Poster,
                    Rating = result.Rating,
                    Votes = result.Votes,
                    // Actors = actors,
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