using System.IO;
using System.Threading;
using System.Threading.Tasks;
using _2C2P.TransactionsManager.Infrastructure;

namespace _2C2P.TransactionsManager.Domain.Service.Abstractions
{
    public interface IUploadService
    {
        Task UploadTransactionsFileAsync(Stream fileStream, FileExtension fileExtension,
            CancellationToken cancellationToken);
    }
}