using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using _2C2P.TransactionsManager.Domain.Model;
using AutoMapper;

namespace _2C2P.TransactionsManager.Infrastructure.Xml
{
    public class XmlFileParser: IFileParser
    {
        private readonly IXMlTransactionFileValidator _transactionFileValidator;
        private readonly IMapper _mapper;

        public XmlFileParser(IXMlTransactionFileValidator transactionFileValidator,
            IMapper mapper)
        {
            _transactionFileValidator = transactionFileValidator;
            _mapper = mapper;
        }

        public FileParseResult Parse(Stream fileStream)
        {
            var xDocument = XDocument.Load(fileStream);

            if (!_transactionFileValidator.IsValid(xDocument, out List<FileValidationResult> validationErrors))
            {
                return new FileParseResult
                {
                    Errors = validationErrors
                };
            }

            XmlSerializer xml = new XmlSerializer(typeof(XmlTransactionsRecord));
            var result = xml.Deserialize(xDocument.CreateReader()) as XmlTransactionsRecord;

            var mappedRecords = result?.Transactions?.ToList();
            var transactions = _mapper.Map<List<Transaction>>(mappedRecords);

            return new FileParseResult
            {
                MappedRecords = transactions
            };
        }

        public bool IsApplicable(FileExtension fileExtension)
        {
            return fileExtension == FileExtension.Xml;
        }
    }
}