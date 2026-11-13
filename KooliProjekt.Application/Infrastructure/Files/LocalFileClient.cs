using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Infrastructure.Files
{
    public class LocalFileClient : IFileClient
    {
        private readonly string _filesRoot;

        public LocalFileClient(string filesRoot)
        {
            _filesRoot = Path.GetFullPath(filesRoot);
        }

        public async Task Save(string fileName, string fileStore, Stream inputStream)
        {
            var folderPath = GetFolderPath(fileStore);
            var filePath = GetFilePath(folderPath, fileName);

            Directory.CreateDirectory(folderPath);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await inputStream.CopyToAsync(fileStream);
        }

        public Task Delete(string fileName, string fileStore)
        {
            var filePath = GetFilePath(GetFolderPath(fileStore), fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Task.CompletedTask;
        }

        public Task<Stream> Get(string fileName, string fileStore)
        {
            var filePath = GetFilePath(GetFolderPath(fileStore), fileName);
            if (!File.Exists(filePath))
            {
                return Task.FromResult<Stream>(null);
            }

            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return Task.FromResult(stream);
        }

        public Task<IList<string>> List(string fileStore)
        {
            var folderPath = GetFolderPath(fileStore);
            IList<string> result = new List<string>();

            if (Directory.Exists(folderPath))
            {
                result = Directory.GetFiles(folderPath)
                    .Select(Path.GetFileName)
                    .OrderBy(name => name)
                    .ToList();
            }

            return Task.FromResult(result);
        }

        private string GetFolderPath(string fileStore)
        {
            var folderPath = Path.GetFullPath(Path.Combine(_filesRoot, fileStore));
            if (!folderPath.StartsWith(_filesRoot, StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Invalid file store.", nameof(fileStore));
            }

            return folderPath;
        }

        private static string GetFilePath(string folderPath, string fileName)
        {
            // Kaitse kataloogist välja pääsemise vastu (../ jms)
            if (string.IsNullOrWhiteSpace(fileName) || fileName != Path.GetFileName(fileName))
            {
                throw new ArgumentException("Invalid file name.", nameof(fileName));
            }

            return Path.Combine(folderPath, fileName);
        }
    }
}
