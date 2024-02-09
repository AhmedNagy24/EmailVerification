using System.Collections;
using System.IO;
using System.Text;
using EmailVerifier.Models;

namespace EmailVerifier.ReaderWriter;

public class CsvWriter : IWriter
{
    public bool Write(ArrayList data, string outputPath)
    {
        var writer = new StreamWriter(outputPath, false, new UTF8Encoding(true));
        writer.WriteLine("Email,Name");
        writer.Close();
        writer = new StreamWriter(outputPath, true, new UTF8Encoding(true));
        foreach (Pair<string, string> row in data)
        {
            var line = row.First + "," + row.Second;
            writer.WriteLine(line);
        }

        writer.Close();
        return true;
    }
}