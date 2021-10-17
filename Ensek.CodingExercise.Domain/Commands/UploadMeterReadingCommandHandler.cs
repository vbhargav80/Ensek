using System.Threading;
using System.Threading.Tasks;
using Ensek.CodingExercise.Domain.Entities;
using Ensek.CodingExercise.Domain.Repository;
using Ensek.CodingExercise.Domain.Responses;
using Ensek.CodingExercise.Domain.Services;
using MediatR;

namespace Ensek.CodingExercise.Domain.Commands
{
    public class UploadMeterReadingCommandHandler : IRequestHandler<UploadMeterReadingCommand, UploadMeterReadingResponse>
    {
        private readonly IFileParsingService _fileParsingService;
        private readonly IValidationService _validationService;
        private readonly IRepository<MeterReading> _meterReadingRepository;

        public UploadMeterReadingCommandHandler(
            IFileParsingService fileParsingService,
            IValidationService validationService,
            IRepository<MeterReading> meterReadingRepository
            )
        {
            _fileParsingService = fileParsingService;
            _validationService = validationService;
            _meterReadingRepository = meterReadingRepository;
        }

        public async Task<UploadMeterReadingResponse> Handle(UploadMeterReadingCommand request, CancellationToken cancellationToken)
        {
            var meterReadingRecords = _fileParsingService.ParseFile(request.FileContents);
            var validationResult = await _validationService.Validate(meterReadingRecords);

            foreach (var meterReading in validationResult.ValidMeterReadings)
            {
                await _meterReadingRepository.AddAsync(meterReading);
            }

            return new UploadMeterReadingResponse
            {
                NumFailedReadings = validationResult.NumInvalidRecords,
                NumSuccessfulReadings = validationResult.NumValidRecords
            };
        }
    }
}
