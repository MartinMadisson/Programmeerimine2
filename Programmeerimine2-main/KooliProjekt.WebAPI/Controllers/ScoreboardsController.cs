using System.Threading.Tasks;
using KooliProjekt.Application.Features.Scoreboards;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class ScoreboardsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ScoreboardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get/{id}")] 
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new GetScoreboardQuery { Id = id });
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result = await _mediator.Send(new ListScoreboardsQuery());
            return Ok(result);
        }
    }
}
