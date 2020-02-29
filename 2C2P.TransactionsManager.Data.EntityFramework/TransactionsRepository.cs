using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2C2P.TransactionsManager.Data.Abstractions;
using _2C2P.TransactionsManager.Data.EntityFramework.Entities;
using _2C2P.TransactionsManager.Domain.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _2C2P.TransactionsManager.Data.EntityFramework
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly IRepository<TransactionEntity> _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionsRepository(IRepository<TransactionEntity> transactionRepository,
            IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<List<Transaction>> GetAllAsync()
        {
            var entities = await _transactionRepository.All().ToListAsync();
            return _mapper.Map<List<Transaction>>(entities);
        }

        public async Task SaveTransactionsAsync(List<Transaction> transactions)
        {
            var entities = _mapper.Map<List<TransactionEntity>>(transactions);
            await _transactionRepository.AddRangeAsync(entities);
        }

        public async Task UpsertTransactionsAsync(List<Transaction> transactions)
        {
            var transactionIds = transactions.Select(transaction => transaction.TransactionId).ToList();

            var transactionIdsToUpdate = _transactionRepository
                .All(entity => transactionIds.Contains(entity.Id))
                .Select(entity => entity.Id)
                .ToList();

            if (transactionIdsToUpdate.Any())
            {
                var transactionsToUpdate = transactions
                    .Where(t => transactionIdsToUpdate.Contains(t.TransactionId))
                    .ToList();

                var entitiesToUpdate = _mapper.Map<List<TransactionEntity>>(transactionsToUpdate);
                await _transactionRepository.UpdateRange(entitiesToUpdate);
            }

            var transactionIdsToAdd = transactionIds.Except(transactionIdsToUpdate);
            var transactionsToAdd =
                transactions
                    .Where(transaction => transactionIdsToAdd.Contains(transaction.TransactionId))
                    .ToList();

            await SaveTransactionsAsync(transactionsToAdd);
        }
    }
}