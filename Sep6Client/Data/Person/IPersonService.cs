using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6Client.Model;

namespace Sep6Client.Data.Person
{
    public interface IPersonService
    {
        Task<Model.Person> GetPersonByIdAsync(int personId);
        Task<PersonList> GetBrowsePersonsAsync(int pageNr);
    }
}