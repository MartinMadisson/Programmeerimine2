using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Teams
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, OperationResult>
    {
        private readonly IFileClient _fileClient;

        public DeleteFileCommandHandler(IFileClient fileClient)
        {
            _fileClient = fileClient;
        }

        public async Task<OperationResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();
            var store = FileStore.ForTeam(request.Id);

            var files = await _fileClient.List(store);
            if (!files.Contains(request.FileName))
            {
                return result.AddPropertyError("FileName", "File not found.");
            }

            await _fileClient.Delete(request.FileName, store);

            return result;
        }
    }
}
