using System.IO;

namespace _2C2P.TransactionsManager.Infrastructure
{
    public interface IFileParser
    {
        FileParseResult Parse(Stream fileStream);

        bool IsApplicable(FileExtension fileExtension);
    }
}
