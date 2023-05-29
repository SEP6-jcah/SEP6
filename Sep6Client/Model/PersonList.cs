using System.Collections.Generic;

namespace Sep6Client.Model
{
    public class PersonList
    {
        public IList<Person> Persons { get; set; }
        public int NrOfPages { get; set; }
        public int NrOfResults { get; set; }
    }
}