using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Teams
{
    public class GetFileCommand : IRequest<OperationResult<FileDto>>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
    }
}
