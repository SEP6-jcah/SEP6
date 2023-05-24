using System;
using Sep6Client.Data.DataHelper.Wrappers;
using Sep6Client.Model;

namespace Sep6Client.Data.DataHelper.Mappers
{
    public static class CrewMemberMapper
    {
        public static CrewMember ToCrewMember(CrewMemberResult result)
        {
            try
            {
                return new CrewMember
                {
                    PersonId = result.PersonId,
                    CreditId = result.CreditId,
                    Name = result.Name,
                    OriginalName = result.OriginalName,
                    PopularityRating = result.PopularityRating,
                    ProfileLink = result.ProfileLink,
                    Department = result.Department,
                    JobDescription = result.JobDescription
                };
            }
            catch (Exception e)
            {
                throw new Exception($"Error mapping result to crew member: {e.Message}\n{e.StackTrace}");
            }
        }
    }
}