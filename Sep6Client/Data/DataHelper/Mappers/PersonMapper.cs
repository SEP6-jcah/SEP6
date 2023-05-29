using System;
using System.Collections.Generic;
using Sep6Client.Data.DataHelper.Wrappers;
using Sep6Client.Model;

namespace Sep6Client.Data.DataHelper.Mappers
{
    public static class PersonMapper
    {
        public static Model.Person ToPerson(PersonResult result)
        {
            try
            {
                return new Model.Person
                {
                    Id = result.Id,
                    Name = result.Name,
                    Aka = result.Aka,
                    OriginalName = result.OriginalName,
                    Gender = result.Gender,
                    Birthday = result.Birthday,
                    Biography = result.Biography,
                    Job = result.Job,
                    KnownFor = new CreditList()
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