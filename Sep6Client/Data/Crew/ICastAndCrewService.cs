using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6Client.Model;

namespace Sep6Client.Data.Crew
{
    public interface ICastAndCrewService
    {
        Task<IList<CrewMember>> GetCrewByMovieIdAsync(int id);
        Task<IList<Actor>> GetActorsByMovieIdAsync(int id);
    }
}