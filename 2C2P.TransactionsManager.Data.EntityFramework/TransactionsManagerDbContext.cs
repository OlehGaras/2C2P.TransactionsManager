using _2C2P.TransactionsManager.Data.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;


namespace _2C2P.TransactionsManager.Data.EntityFramework
{
    public class TransactionsManagerDbContext: DbContext
    {
        public DbSet<TransactionEntity> Transactions { get; set; }

        public TransactionsManagerDbContext()
        {
        }

        public TransactionsManagerDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
