using Ensek.CodingExercise.Domain.Responses;
using MediatR;

namespace Ensek.CodingExercise.Domain.Commands
{
    public class UploadMeterReadingCommand : IRequest<UploadMeterReadingResponse>
    {
        public byte[] FileContents { get; set; }
    }
}
