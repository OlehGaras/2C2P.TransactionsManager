using System;
using System.IO;
using System.Linq;
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

        public async Task<ServiceResult> UploadTransactionsFileAsync(Stream fileStream, FileExtension fileExtension,
            CancellationToken cancellationToken)
        {
            try
            {
                var parseResult = _fileParseStrategy.Parse(fileStream, fileExtension);
                if (parseResult.IsValid)
                {
                    var transactions = parseResult.MappedRecords;
                    return await _transactionsService.UpsertTransactionsAsync(transactions, cancellationToken);
                }

                var parseErrors = parseResult.Errors.Select(result =>
                    $"Message: {string.Join('|', result.Messages)}; Record: {result.UnmappedRecord}").ToList();

                parseErrors.ForEach(error => _logger.LogError(error));
                
                return new ServiceResult<bool>()
                {
                    Errors = parseErrors
                };
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