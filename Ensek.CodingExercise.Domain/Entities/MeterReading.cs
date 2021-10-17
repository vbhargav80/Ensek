using System;

namespace Ensek.CodingExercise.Domain.Entities
{
    public class MeterReading : IEntity, IEquatable<MeterReading>
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime? ReadingDate { get; set; }
        public string MeterReadValue { get; set; }
        public Account Account { get; set; }
        public bool Equals(MeterReading other)
        {
            if (other == null)
                return false;

            return AccountId.Equals(other.AccountId) &&
                   ReadingDate.Equals(other.ReadingDate) &&
                   (
                       ReferenceEquals(MeterReadValue, other.MeterReadValue) ||
                       MeterReadValue != null && MeterReadValue.Equals(other.MeterReadValue)
                   );
        }
    }
}
