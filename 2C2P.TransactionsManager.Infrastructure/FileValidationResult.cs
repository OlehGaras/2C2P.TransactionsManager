﻿using System.Collections.Generic;
using System.Linq;

namespace _2C2P.TransactionsManager.Infrastructure
{
    public class FileValidationResult
    {
        public List<string> Messages { get; set; } = new List<string>();
        public string UnmappedRecord { get; set; }
        public bool HasErrors => Messages.Any();
    }
}