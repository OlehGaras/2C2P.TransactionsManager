using System.Collections.Generic;
using System.Linq;
using _2C2P.TransactionsManager.Domain.Model;

namespace _2C2P.TransactionsManager.Infrastructure
{
    public class FileParseResult
    {
        public List<Transaction> MappedRecords { get; set; }

        public bool IsValid => Errors == null || !Errors.Any();

        public List<FileValidationResult> Errors { get; set; }
    }
}