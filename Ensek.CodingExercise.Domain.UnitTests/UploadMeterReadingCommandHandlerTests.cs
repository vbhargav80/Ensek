using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ensek.CodingExercise.Domain.Commands;
using Ensek.CodingExercise.Domain.Entities;
using Ensek.CodingExercise.Domain.Repository;
using Ensek.CodingExercise.Domain.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ensek.CodingExercise.Domain.UnitTests
{
    public class UploadMeterReadingCommandHandlerTests
    {
        private readonly UploadMeterReadingCommandHandler _commandHandler;
        private readonly IFileParsingService _fileParsingService;
        private readonly IValidationService _validationService;
        private readonly IRepository<MeterReading> _meterReadingRepository;
        private readonly IRepository<Account> _accountRepository;

        public UploadMeterReadingCommandHandlerTests()
        {
            _fileParsingService = Substitute.For<IFileParsingService>();
            _meterReadingRepository = Substitute.For<IRepository<MeterReading>>();
            _accountRepository = Substitute.For<IRepository<Account>>();
            _validationService = new ValidationService(_accountRepository, _meterReadingRepository);

            _commandHandler = new UploadMeterReadingCommandHandler(_fileParsingService, _validationService, _meterReadingRepository);
        }

        [Fact]
        public async Task Ignores_InvalidRecordsFromCsvParsing()
        {
            var mockDate = DateTime.Now;
            var mockMeterReads = new List<MeterReading>()
            {
                new MeterReading {AccountId = 5, MeterReadValue = "A9091", ReadingDate = mockDate},
                new MeterReading {AccountId = 6, MeterReadValue = "09091", ReadingDate = mockDate},
                new MeterReading {AccountId = 7, MeterReadValue = "090910", ReadingDate = mockDate},
            };

            var mockAccounts = new List<Account>
            {
                new Account() {Id = 5},
                new Account() {Id = 6},
                new Account() {Id = 7},
            };

            _fileParsingService
                .ParseFile(Arg.Any<byte[]>())
                .Returns(mockMeterReads);

            _meterReadingRepository
                .GetAllAsync()
                .Returns(Task.FromResult<IList<MeterReading>>(null));

            _accountRepository
                .GetAllAsync()
                .Returns(Task.FromResult<IList<Account>>(mockAccounts));

            var response = await _commandHandler.Handle(new UploadMeterReadingCommand(), CancellationToken.None);
            response.NumFailedReadings.Should().Be(2);
            response.NumSuccessfulReadings.Should().Be(1);
        }

        [Fact]
        public async Task Ignores_InvalidAccountNumbers()
        {
            var mockDate = DateTime.Now;
            var mockMeterReads = new List<MeterReading>()
            {
                new MeterReading {AccountId = 5, MeterReadValue = "08087", ReadingDate = mockDate},
                new MeterReading {AccountId = 6, MeterReadValue = "09091", ReadingDate = mockDate},
                new MeterReading {AccountId = 7, MeterReadValue = "08876", ReadingDate = mockDate},
            };

            var mockAccounts = new List<Account>
            {
                new Account() {Id = 5},
                new Account() {Id = 6}
            };

            _fileParsingService
                .ParseFile(Arg.Any<byte[]>())
                .Returns(mockMeterReads);

            _meterReadingRepository
                .GetAllAsync()
                .Returns(Task.FromResult<IList<MeterReading>>(null));

            _accountRepository
                .GetAllAsync()
                .Returns(Task.FromResult<IList<Account>>(mockAccounts));

            var response = await _commandHandler.Handle(new UploadMeterReadingCommand(), CancellationToken.None);
            response.NumFailedReadings.Should().Be(1);
            response.NumSuccessfulReadings.Should().Be(2);
        }
    }
}
