using EmailVerifier.Models;

namespace EmailVerifier.ReaderWriter;

public interface IWriter
{
    public bool Write(List<Pair<string, string>> invalid, string outputPath);
}