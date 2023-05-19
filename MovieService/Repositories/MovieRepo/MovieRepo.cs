using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieService.Persistence;

namespace MovieService.Repositories.MovieRepo
{
    public class MovieRepo : IMovieRepo
    {
        private readonly MovieDbContext persistence;

        public MovieRepo(MovieDbContext movieDbContext)
        {
            persistence = movieDbContext;
        }

        public async Task<IList<Movie>> GetAllMoviesAsync()
        {
            if (persistence.Movies != null)
            {
                var movieSet = await persistence.Movies.ToArrayAsync();
                return movieSet.ToList();
            }

            var testMovie = new Movie
            {
                Id = 123,
                Title = "Test",
                Year = 2023
            };
            return new List<Movie>{testMovie};
        }

        public async Task<IList<Movie>> GetNext25MoviesAsync(int startIndex)
        {
            Movie[] movies = new Movie[25];
            if (persistence != null)
            {
                var movieSet = await persistence.Movies.ToArrayAsync();

                try
                {
                    for (int i = 0; i < 25; i++)
                    {
                        movies[i] = movieSet[startIndex + i];
                    }
                    
                    return movies;
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new IndexOutOfRangeException("No more movies to display.", e);
                }
            }

            for (int i = 0; i < 25; i++)
            {
                movies[i] = new Movie {Id = i, Title = $"Test Movie {i}", Year = 1950 + i};
            }

            return movies;
        }
        
        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            Movie? movie = null;
            if (persistence.Movies != null)
            {
                movie = await persistence.Movies.FindAsync(id);
            }

            if (movie == null)
            {
                throw new NullReferenceException($"Movie with ID {id} not found.");
            }
            return movie;
        }
    }
}