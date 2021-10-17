using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Ensek.CodingExercise.Domain.Entities;
using Ensek.CodingExercise.Domain.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ensek.CodingExercise.Infrastructure
{
    public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable($"{nameof(Account)}");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.HasMany(x => x.MeterReadings).WithOne(a => a.Account);

            builder.HasData(SeedAccoundData());
        }

        public List<Account> SeedAccoundData()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "SeedData", "Test_Accounts.csv");
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<AccountMap>();

            return csv.GetRecords<Account>().ToList();
        }
    }
}
