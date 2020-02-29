using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using _2C2P.TransactionsManager.Data.Abstractions;
using _2C2P.TransactionsManager.Domain.Model;
using _2C2P.TransactionsManager.Domain.Service.Abstractions;
using Microsoft.Extensions.Logging;

namespace _2C2P.TransactionsManager.Domain.Service.Implementations
{
    public class TransactionsService: ITransactionsService
    {
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly ILogger<TransactionsService> _logger;

        public TransactionsService(ITransactionsRepository transactionsRepository,
            ILogger<TransactionsService> logger)
        {
            _transactionsRepository = transactionsRepository;
            _logger = logger;
        }

        public async Task<ServiceResult> UpsertTransactionsAsync(List<Transaction> transactions,
            CancellationToken cancellationToken = default)
        {
            var result = new ServiceResult();
            try
            {
                var duplicates = transactions
                    .GroupBy(transaction => transaction.TransactionId)
                    .Where(group => group.Count() > 1)
                    .Select(group => group.Key)
                    .ToList();

                if (duplicates.Any())
                {
                    result.Errors.Add($"Duplicated transaction Ids found: {string.Join(',', duplicates)}");
                    return result;
                }

                await _transactionsRepository.UpsertTransactionsAsync(transactions);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex,
                    $"Error occured during upserting transactions: {nameof(TransactionsService)}.");
                throw;
            }
        }
    }
}