using System.Collections;

namespace EmailVerifier.ReaderWriter;

public interface IWriter
{
    public bool Write(ArrayList data, string outputPath);
}