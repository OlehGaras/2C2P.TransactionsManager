using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using _2C2P.TransactionsManager.Domain.Service.Abstractions;
using _2C2P.TransactionsManager.Infrastructure;
using Microsoft.Extensions.Logging;

namespace _2C2P.TransactionsManager.Domain.Service.Implementations
{
    public class UploadService: IUploadService
    {
        private readonly IFileParseStrategy _fileParseStrategy;
        private readonly ITransactionsService _transactionsService;
        private readonly ILogger<UploadService> _logger;

        public UploadService(
            IFileParseStrategy fileParseStrategy,
            ITransactionsService transactionsService,
            ILogger<UploadService> logger)
        {
            _fileParseStrategy = fileParseStrategy;
            _transactionsService = transactionsService;
            _logger = logger;
        }

        public async Task UploadTransactionsFileAsync(Stream fileStream, FileExtension fileExtension,
            CancellationToken cancellationToken)
        {
            try
            {
                var transactions = _fileParseStrategy.Parse(fileStream, fileExtension);

                await _transactionsService.UpsertTransactionsAsync(transactions, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, 
                    $"Error occured importing transactions file: {nameof(UploadService)}.");
                throw;
            }
        }
    }
}