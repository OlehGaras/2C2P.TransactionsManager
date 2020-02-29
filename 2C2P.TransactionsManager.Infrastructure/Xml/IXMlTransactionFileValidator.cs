using System.Collections.Generic;
using System.Xml.Linq;

namespace _2C2P.TransactionsManager.Infrastructure.Xml
{
    public interface IXMlTransactionFileValidator
    {
        bool IsValid(XDocument xDocument, out List<FileValidationResult> validationErrors);
    }
}