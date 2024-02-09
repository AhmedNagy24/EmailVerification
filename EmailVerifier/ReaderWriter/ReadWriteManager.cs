using System.Collections;

namespace EmailVerifier.ReaderWriter;

public class ReadWriteManager
{
    public IReader? Reader { get; set; }
    public IWriter? Writer { get; set; }

    public ArrayList? Read(string path)
    {
        return Reader?.Read(path);
    }

    public bool Write(ArrayList data, string path)
    {
        return Writer != null && Writer.Write(data, path);
    }
}