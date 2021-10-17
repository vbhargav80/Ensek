using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ensek.CodingExercise.Domain.Entities;
using Ensek.CodingExercise.Domain.Models;

namespace Ensek.CodingExercise.Domain.Services
{
    public interface IValidationService
    {
        Task<ValidationResult> Validate(List<MeterReading> meterReadings);
    }
}
