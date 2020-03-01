using System.Collections.Generic;
using System.IO;
using _2C2P.TransactionsManager.Domain.Model;

namespace _2C2P.TransactionsManager.Infrastructure
{
    public interface IFileParser
    {
        List<Transaction> Parse(Stream fileStream);

        bool IsApplicable(FileExtension fileExtension);
    }
}
