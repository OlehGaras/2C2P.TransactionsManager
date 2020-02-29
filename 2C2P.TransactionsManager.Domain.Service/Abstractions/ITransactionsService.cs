using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using _2C2P.TransactionsManager.Domain.Model;

namespace _2C2P.TransactionsManager.Domain.Service.Abstractions
{
    public interface ITransactionsService
    {
        Task<ServiceResult> UpsertTransactionsAsync(List<Transaction> transactions,
            CancellationToken cancellationToken = default);
    }
}
