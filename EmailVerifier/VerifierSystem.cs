using System.Collections;
using System.ComponentModel;
using EmailVerifier.Models;
using EmailVerifier.ReaderWriter;
using EmailVerifier.Verify;

namespace EmailVerifier;

public class VerifierSystem
{
    public int Run(bool dnsCheck, bool formatCheck, string dirName, object? sender, DoWorkEventArgs e)
    {
        var readWrite = new ReadWriteManager();
        readWrite.Reader = new DirReader();
        var files = readWrite.Read(dirName);
        if (files == null) throw new Exception("VerifierSystem.Run: Directory has no files!");
        readWrite.Reader = new CsvReader();
        var verifyManager = new VerifyManager(new FormatValidator());
        var count = 0;
        foreach (string file in files)
        {
            if ((sender as BackgroundWorker)?.CancellationPending == true)
            {
                e.Cancel = true;
                return -1;
            }

            (sender as BackgroundWorker)?.ReportProgress(0, file);
            var data = readWrite.Read(file);
            if (data == null) continue;
            try
            {
                List<Pair<string, string>> toBeRemoved;
                if (formatCheck)
                {
                    toBeRemoved = verifyManager.Verify(data, sender, data.Count);
                    data = UpdateContacts(data, toBeRemoved);
                    (sender as BackgroundWorker)?.ReportProgress(0);
                    count += toBeRemoved.Count;
                }

                if (dnsCheck)
                {
                    verifyManager.Validator = new DnsValidator();
                    toBeRemoved = verifyManager.Verify(data, sender, data.Count);
                    data = UpdateContacts(data, toBeRemoved);
                    (sender as BackgroundWorker)?.ReportProgress(0);
                    count += toBeRemoved.Count;
                }

                if ((sender as BackgroundWorker)?.CancellationPending == true) throw new UserCancelException();
                readWrite.Writer = new CsvWriter();
                readWrite.Write(data, file);
            }
            catch (UserCancelException)
            {
                e.Cancel = true;
                return -1;
            }
        }

        return count;
    }

    private static ArrayList UpdateContacts(ArrayList contacts, List<Pair<string, string>> toBeRemoved)
    {
        foreach (var contact in toBeRemoved) contacts.Remove(contact);
        return contacts;
    }
}