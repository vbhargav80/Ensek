using System.Collections.Generic;

namespace Ensek.CodingExercise.Domain.Entities
{
    public class Account : IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<MeterReading> MeterReadings { get; set; }
        public int Id { get; set; }
    }
}
