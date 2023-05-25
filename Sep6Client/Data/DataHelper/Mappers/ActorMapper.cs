using System;
using Sep6Client.Data.DataHelper.Wrappers;
using Sep6Client.Model;

namespace Sep6Client.Data.DataHelper.Mappers
{
    public static class ActorMapper
    {
        public static Actor ToActor(ActorResult result)
        {
            try
            {
                return new Actor
                {
                    PersonId = result.PersonId,
                    ActorId = result.ActorId,
                    CreditId = result.CreditId,
                    Character = result.Character,
                    Name = result.Name,
                    OriginalName = result.OriginalName,
                    PopularityRating = result.PopularityRating,
                    ProfileLink = result.ProfileLink,
                    Department = result.Department
                };
            }
            catch (Exception e)
            {
                throw new Exception($"Error mapping result to actor: {e.Message}\n{e.StackTrace}");
            }
        }
    }
}