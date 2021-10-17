using System;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Ensek.CodingExercise.Domain.Entities;

namespace Ensek.CodingExercise.Domain.Mappings
{
    public class MeterReadingMap : ClassMap<MeterReading>
    {
        public MeterReadingMap()
        {
            Map(m => m.AccountId).Name("AccountId");
            Map(m => m.ReadingDate).Name("MeterReadingDateTime").TypeConverter<DateTimeConverter>();
            Map(m => m.MeterReadValue).Name("MeterReadValue");
        }
    }

    public class DateTimeConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            DateTime? readingDate = null;

            if (DateTime.TryParseExact(text, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                readingDate = parsedDate;
            }

            return readingDate;
        }
    }
}
