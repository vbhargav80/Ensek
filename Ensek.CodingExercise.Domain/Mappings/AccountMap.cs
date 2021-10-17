using CsvHelper.Configuration;
using Ensek.CodingExercise.Domain.Entities;

namespace Ensek.CodingExercise.Domain.Mappings
{
    public class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Map(m => m.Id).Name("AccountId");
            Map(m => m.FirstName).Name("FirstName");
            Map(m => m.LastName).Name("LastName");
        }
    }
}
