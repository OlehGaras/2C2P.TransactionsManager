using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _2C2P.TransactionsManager.Domain.Model;
using _2C2P.TransactionsManager.Domain.Service.Abstractions;

namespace _2C2P.TransactionsManager.Domain.Service.Implementations
{
    public class TransactionsService: ITransactionsService
    {
        public Task<ServiceResult> UpsertTransactionsAsync(List<Transaction> transactions, 
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new ServiceResult());
        }
    }
}