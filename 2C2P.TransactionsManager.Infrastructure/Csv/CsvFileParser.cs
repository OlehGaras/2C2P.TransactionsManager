using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using _2C2P.TransactionsManager.Domain.Model;
using AutoMapper;
using TinyCsvParser;

namespace _2C2P.TransactionsManager.Infrastructure.Csv
{
    public class CsvFileParser : IFileParser
    {
        private readonly IMapper _mapper;

        public CsvFileParser(IMapper mapper)
        {
            _mapper = mapper;
        }

        public FileParseResult Parse(Stream fileStream)
        {
            var csvParserOptions = new CsvParserOptions(false, ',');
            var csvMapper = new CsvTransactionDtoMapping();
            var csvParser = new CsvParser<CsvTransactionRecord>(csvParserOptions, csvMapper);

            var results = csvParser
                .ReadFromStream(fileStream, Encoding.UTF8)
                .ToList();

            var validationResults = results
                    .Where(result => !result.IsValid)
                    .Select(result =>
                    {
                        var validationResult = new FileValidationResult()
                        {
                            UnmappedRecord = result.Error.UnmappedRow
                        };
                        validationResult.Messages.Add(result.Error.Value);

                        return validationResult;
                    }).ToList();

            if (validationResults.Any())
            {
                return new FileParseResult()
                {
                    Errors = validationResults
                };
            }

            var mappedRecords = results
                .Where(result => result.IsValid)
                .Select(result => result.Result)
                .ToList();

            var transactions = _mapper.Map<List<Transaction>>(mappedRecords);

            return new FileParseResult
            {
                MappedRecords = transactions
            };
        }

        public bool IsApplicable(FileExtension fileExtension)
        {
            return fileExtension == FileExtension.Csv;
        }
    }
}