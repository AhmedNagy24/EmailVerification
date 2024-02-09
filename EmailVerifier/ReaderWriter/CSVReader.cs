using System.Collections;
using System.IO;
using System.Text;
using EmailVerifier.Models;
using Microsoft.VisualBasic.FileIO;

namespace EmailVerifier.ReaderWriter;

public class CsvReader : IReader
{
    public ArrayList? Read(string path)
    {
        var data = new ArrayList();
        if (!File.Exists(path)) return null;
        try
        {
            var reader = new StreamReader(path, Encoding.UTF8);
            var parser = new TextFieldParser(reader);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            parser.ReadFields();
            while (!parser.EndOfData)
            {
                var row = parser.ReadFields();
                if (row == null) continue;
                var pair = new Pair<string, string>(row[0], row[1]);
                data.Add(pair);
            }

            parser.Close();
            reader.Close();
            return data;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}