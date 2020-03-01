using System;
using System.Collections.Generic;

namespace _2C2P.TransactionsManager.Infrastructure
{
    public class FileParseException : Exception
    {
        public List<FileValidationResult> ErrorResults { get; }
        public FileParseException(string message, List<FileValidationResult> errorResults) : base(message)
        {
            this.ErrorResults = errorResults;
        }
    }
}