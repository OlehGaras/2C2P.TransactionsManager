using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _2C2P.TransactionsManager.Domain.Model;
using _2C2P.TransactionsManager.Domain.Service.Filters;

namespace _2C2P.TransactionsManager.Domain.Service.Abstractions
{
    public interface ITransactionsService
    {
        Task<ServiceResult> UpsertTransactionsAsync(List<Transaction> transactions,
            CancellationToken cancellationToken = default);

        Task<ServiceResult<List<Transaction>>> GetAllAsync(TransactionsFilter transactionsFilter = null);
    }
}
