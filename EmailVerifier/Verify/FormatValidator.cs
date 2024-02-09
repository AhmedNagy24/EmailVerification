using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using EmailVerifier.Models;

namespace EmailVerifier.Verify;

public class FormatValidator : IValidator
{
    public List<Pair<string, string>> Verify(ArrayList data, object? sender, int max)
    {
        const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        var toBeRemoved = new List<Pair<string, string>>();
        var index = 1.0;
        foreach (Pair<string, string> pair in data)
        {
            if ((sender as BackgroundWorker)?.CancellationPending == true) throw new UserCancelException();
            var regex = new Regex(pattern);
            if (!regex.IsMatch(pair.First)) toBeRemoved.Add(pair);
            var percent = (int)(index / max * 100);
            (sender as BackgroundWorker)?.ReportProgress(percent);
            index++;
        }

        return toBeRemoved;
    }
}