using System.Collections.Generic;
using System.Threading.Tasks;
using Sep6Client.Data.DataHelper.Search;
using Sep6Client.Model;

namespace Sep6Client.Data.Person
{
    public interface IPersonService
    {
        Task<Model.Person> GetPersonByIdAsync(int personId);
        Task<PersonList> GetBrowsePersonsAsync(Dictionary<SearchFilterOptions, string> criteria);
        Task<PersonList> GetFilteredPersonsAsync(Dictionary<SearchFilterOptions, string> criteria);
    }
}