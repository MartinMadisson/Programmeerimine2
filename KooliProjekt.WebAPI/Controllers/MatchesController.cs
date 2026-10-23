using System.Threading.Tasks;
using KooliProjekt.Application.Features.Matches;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace KooliProjekt.WebAPI.Controllers
{
    public class MatchesController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public MatchesController(IMediator mediator) { _mediator = mediator; }

        [HttpGet("get/{id}")] 
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return NotFound();
            var result = await _mediator.Send(new GetMatchQuery { Id = id });
            return Result(result);
        }

        [HttpGet("list/{page:int}/{pageSize:int}")] 
        public async Task<IActionResult> List(int page, int pageSize)
        {
            var result = await _mediator.Send(new ListMatchesQuery { Page = page, PageSize = pageSize });
            return Result(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListDefault()
        {
            var result = await _mediator.Send(new ListMatchesQuery());
            return Result(result);
        }
    }
}
