using System;
using System.Collections.Generic;
using System.Linq;
using Sep6Client.Data.DataHelper.Wrappers;
using Sep6Client.Model;

namespace Sep6Client.Data.DataHelper.Mappers
{
    public static class CreditMapper
    {
        public static CreditList ToCreditList(CreditListResult result)
        {
            try
            {
                var crew = result.CrewCredits.Select(crewCredits => new CrewCredits
                    {
                        MovieId = crewCredits.MovieId,
                        MovieTitle = crewCredits.MovieTitle,
                        MovieReleaseDate = crewCredits.MovieReleaseDate,
                        MovieRating = crewCredits.MovieRating,
                        Votes = crewCredits.Votes,
                        Job = crewCredits.Job,
                        Department = crewCredits.Department
                    })
                    .ToList();

                var actors = result.ActorCredits.Select(actorCredits => new ActorCredits
                    {
                        MovieId = actorCredits.MovieId,
                        MovieTitle = actorCredits.MovieTitle,
                        MovieReleaseDate = actorCredits.MovieReleaseDate,
                        MovieRating = actorCredits.MovieRating,
                        Votes = actorCredits.Votes,
                        Character = actorCredits.Character
                    })
                    .ToList();
                return new CreditList
                {
                    ActorCredits = actors,
                    CrewCredits = crew
                };
            }
            catch (Exception e)
            {
                Console.WriteLine("Error mapping result to credits in credit mapper");
                throw;
            }
        }
    }
}