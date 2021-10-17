using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Ensek.CodingExercise.Domain.Entities;
using Ensek.CodingExercise.Domain.Mappings;

namespace Ensek.CodingExercise.Domain.Services
{
    public class FileParsingService : IFileParsingService
    {
        public List<MeterReading> ParseFile(byte[] fileContents)
        {
            using (var ms = new MemoryStream(fileContents))
            {
                using (var reader = new StreamReader(ms, true))
                {
                    using (var csvReader = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        csvReader.Context.RegisterClassMap<MeterReadingMap>();
                        return csvReader.GetRecords<MeterReading>().ToList();
                    }
                }
            }

        }
    }
}
