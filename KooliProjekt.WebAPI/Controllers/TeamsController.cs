using System;
using System.IO;
using System.Threading.Tasks;
using KooliProjekt.Application.Features.Teams;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace KooliProjekt.WebAPI.Controllers
{
    public class TeamsController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public TeamsController(IMediator mediator) { _mediator = mediator; }

        [HttpGet("get/{id}")] 
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return NotFound();
            var result = await _mediator.Send(new GetTeamQuery { Id = id });
            return Result(result);
        }

        [HttpGet("list/{page:int}/{pageSize:int}")] 
        public async Task<IActionResult> List(int page, int pageSize)
        {
            var result = await _mediator.Send(new ListTeamsQuery { Page = page, PageSize = pageSize });
            return Result(result);
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListDefault()
        {
            var result = await _mediator.Send(new ListTeamsQuery());
            return Result(result);
        }

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] SaveTeamCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteTeamCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }
    
        // ---------- Files ----------

        [HttpPost("addfile")]
        public async Task<IActionResult> AddFile(int id, IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("{\"propertyErrors\":{\"inputStream\":\"File is required.\"}}");
            }

            // Salvestame faili unikaalse nimega, et nimed ei põrkuks
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid().ToString() + extension;

            using var inputStream = file.OpenReadStream();

            var command = new AddFileCommand
            {
                Id = id,
                FileName = fileName,
                InputStream = inputStream
            };

            var result = await _mediator.Send(command);
            if (result.HasErrors)
            {
                return Result(result);
            }

            return Ok(new { fileName });
        }

        [HttpDelete("deletefile")]
        public async Task<IActionResult> DeleteFile([FromBody] DeleteFileCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }

        [HttpGet("getfile")]
        public async Task<IActionResult> GetFile([FromQuery] GetFileCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.HasErrors || result.Value == null)
            {
                return Result(result);
            }

            return File(result.Value.Content, GetContentType(result.Value.FileName));
        }

        [HttpGet("listfiles")]
        public async Task<IActionResult> ListFiles([FromQuery] ListFilesQuery query)
        {
            var result = await _mediator.Send(query);
            return Result(result);
        }

        private static string GetContentType(string fileName)
        {
            switch (Path.GetExtension(fileName).ToLowerInvariant())
            {
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".png": return "image/png";
                case ".gif": return "image/gif";
                case ".webp": return "image/webp";
                default: return "application/octet-stream";
            }
        }
    }
}
