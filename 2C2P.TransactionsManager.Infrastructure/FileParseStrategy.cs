using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace _2C2P.TransactionsManager.Infrastructure
{
    public class FileParseStrategy: IFileParseStrategy
    {
        private readonly ILogger<FileParseStrategy> _logger;
        private readonly IEnumerable<IFileParser> _fileParsers;

        public FileParseStrategy(IServiceProvider serviceProvider,
            ILogger<FileParseStrategy> logger)
        {
            _logger = logger;
            _fileParsers = serviceProvider.GetServices<IFileParser>();
        }

        public FileParseResult Parse(Stream fileStream, FileExtension extension)
        {
            try
            {
                var fileParser = _fileParsers.FirstOrDefault(parser => parser.IsApplicable(extension));

                if (fileParser == null)
                {
                    throw new ArgumentException($"No file parser found to handle the file with extension: {extension}");
                }

                return fileParser.Parse(fileStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while trying to find file parser for extension: {extension}.");
                throw;
            }
        }
    }
}