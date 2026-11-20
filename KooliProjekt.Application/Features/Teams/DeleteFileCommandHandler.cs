using System;
using System.IO;
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
            var existingName = files.FirstOrDefault(f =>
                string.Equals(f, request.FileName, StringComparison.OrdinalIgnoreCase));

            if (existingName == null)
            {
                return result.AddPropertyError("FileName", "File not found.");
            }

            try
            {
                await _fileClient.Delete(existingName, store);
            }
            catch (IOException)
            {
                // Fail on mõne teise programmi poolt avatud (nt pildivaatur või Explorer)
                return result.AddPropertyError("FileName", "The file is in use by another program. Close it and try again.");
            }

            return result;
        }
    }
}
