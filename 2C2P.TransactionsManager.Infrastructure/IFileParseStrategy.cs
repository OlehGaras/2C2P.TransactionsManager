using System.IO;

namespace _2C2P.TransactionsManager.Infrastructure
{
    public interface IFileParseStrategy
    {
        FileParseResult Parse(Stream fileStream, FileExtension extension);
    }
}