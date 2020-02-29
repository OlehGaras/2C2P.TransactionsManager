using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _2C2P.TransactionsManager.Domain.Model;

namespace _2C2P.TransactionsManager.Data.Abstractions
{
    public interface ITransactionsRepository
    {
        Task SaveTransactionsAsync(List<Transaction> transaction);
        Task UpsertTransactionsAsync(List<Transaction> transactions);
        Task<List<Transaction>> GetAllAsync();
    }
}