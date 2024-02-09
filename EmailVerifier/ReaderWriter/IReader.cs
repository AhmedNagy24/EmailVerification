using System.Collections;

namespace EmailVerifier.ReaderWriter;

public interface IReader
{
    public ArrayList? Read(string path);
}