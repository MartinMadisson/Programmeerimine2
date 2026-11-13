using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Teams
{
    public class DeleteFileCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
    }
}
