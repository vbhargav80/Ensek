using System.Collections.Generic;
using Ensek.CodingExercise.Domain.Entities;

namespace Ensek.CodingExercise.Domain.Models
{
    public class ValidationResult
    {
        public int NumValidRecords { get; set; }
        public int NumInvalidRecords { get; set; }
        public List<MeterReading> ValidMeterReadings { get; set; } = new List<MeterReading>();
    }
}
