using System.Collections;
using System.IO;

namespace EmailVerifier.ReaderWriter;

public class DirReader : IReader
{
    public ArrayList? Read(string path)
    {
        try
        {
            if (!Directory.Exists(path)) return null;
            var files = Directory.GetFiles(path);
            return new ArrayList(files);
        }
        catch (Exception)
        {
            return null;
        }
    }
}