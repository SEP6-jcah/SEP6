using System;
using Sep6Client.Data.DataHelper.Wrappers;

namespace Sep6Client.Data.DataHelper.Mappers
{
    public static class PersonMapper
    {
        // public static Model.Person ToPerson(string person)
        // {
        //     return new Model.Person
        //     {
        //         Id = 0,
        //         Name = person,
        //         Gender = 1,
        //         Birthday = "01-01-01",
        //         Department = "idk"
        //     };
        // }
        public static Model.Person ToPerson(PersonResult result)
        {
            try
            {
                return new Model.Person
                {
                    Id = result.Id,
                    Name = result.Name,
                    Gender = result.Gender,
                    Birthday = result.Birthday,
                    Biography = result.Biography,
                    Department = result.Department,
                    KnownFor = result.KnownFor
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}