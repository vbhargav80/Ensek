using System.Collections.Generic;
using Ensek.CodingExercise.Domain.Entities;

namespace Ensek.CodingExercise.Domain.Services
{
    public interface IFileParsingService
    {
        List<MeterReading> ParseFile(byte[] fileContents);
    }
}
