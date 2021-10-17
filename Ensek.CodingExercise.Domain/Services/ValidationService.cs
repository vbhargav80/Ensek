using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ensek.CodingExercise.Domain.Entities;
using Ensek.CodingExercise.Domain.Models;
using Ensek.CodingExercise.Domain.Repository;

namespace Ensek.CodingExercise.Domain.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<MeterReading> _meterReadingRepository;

        public ValidationService(IRepository<Account> accountRepository, IRepository<MeterReading> meterReadingRepository)
        {
            _accountRepository = accountRepository;
            _meterReadingRepository = meterReadingRepository;
        }

        public async Task<ValidationResult> Validate(List<MeterReading> meterReadings)
        {
            var validationResult = new ValidationResult();

            var allAccountsDictionary = (await _accountRepository.GetAllAsync()).ToDictionary(x => x.Id);
            var existingMeterReadings = await _meterReadingRepository.GetAllAsync();

            foreach (var meterReading in meterReadings)
            {
                var isValidReading = !string.IsNullOrEmpty(meterReading.MeterReadValue)
                                     && meterReading.MeterReadValue.Length == 5
                                     && int.TryParse(meterReading.MeterReadValue, out var meterReadingValue);

                var accountExists = allAccountsDictionary.ContainsKey(meterReading.AccountId);
                var meterReadingExists = existingMeterReadings?.Any(x => x.Equals(meterReading)) == true;

                if (meterReading.ReadingDate == null || !isValidReading || !accountExists || meterReadingExists)
                {
                    validationResult.NumInvalidRecords++;
                }
                else
                {
                    validationResult.NumValidRecords++;
                    validationResult.ValidMeterReadings.Add(meterReading);
                }
            }

            return validationResult;
        }
    }
}
