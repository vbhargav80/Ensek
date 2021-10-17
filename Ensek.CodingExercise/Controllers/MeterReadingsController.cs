using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ensek.CodingExercise.Domain.Commands;
using Ensek.CodingExercise.Domain.Responses;
using Ensek.CodingExercise.Extensions;
using MediatR;

namespace Ensek.CodingExercise.Controllers
{
    [Route("api/meter-reading-uploads")]
    [ApiController]
    public class MeterReadingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeterReadingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<ActionResult<UploadMeterReadingResponse>> Upload(IFormFile file)
        {
            var command = new UploadMeterReadingCommand
            {
                FileContents = file.ToByteArray()
            };

            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}
