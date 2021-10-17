using System.Reflection;
using Ensek.CodingExercise.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ensek.CodingExercise.Infrastructure
{
    public class EnsekDbContext : DbContext
    {
        public EnsekDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
