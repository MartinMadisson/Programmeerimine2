using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Infrastructure.Files
{
    public interface IFileClient
    {
        Task Save(string fileName, string fileStore, Stream inputStream);
        Task Delete(string fileName, string fileStore);
        // Tagastab null, kui faili pole. Kutsuja peab voo sulgema.
        Task<Stream> Get(string fileName, string fileStore);
        Task<IList<string>> List(string fileStore);
    }
}
