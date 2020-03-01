using System.Collections.Generic;
using System.IO;
using _2C2P.TransactionsManager.Domain.Model;

namespace _2C2P.TransactionsManager.Infrastructure
{
    public interface IFileParseStrategy
    {
        List<Transaction> Parse(Stream fileStream, FileExtension extension);
    }
}