using System.Collections;
using EmailVerifier.Models;

namespace EmailVerifier.ReaderWriter;

public class ReadWriteManager
{
    public IReader? Reader { get; set; }
    public IWriter? Writer { get; set; }

    public ArrayList? Read(string path)
    {
        return Reader?.Read(path);
    }

    public bool Write(List<Pair<string, string>> invalid, string path)
    {
        return Writer != null && Writer.Write(invalid, path);
    }
}