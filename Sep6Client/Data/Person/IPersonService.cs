using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6Client.Model;

namespace Sep6Client.Data.Person
{
    public interface IPersonService
    {
        Task<IList<Model.Person>> GetCrewByMovieId(int movieId);
        Task<Model.Person> GetPersonByIdAsync(int personId);
        Task<IList<Model.Person>> GetBrowsePersonsAsync(int pageNr);
    }
}