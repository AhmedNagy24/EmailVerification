using System.Collections;
using System.ComponentModel;
using System.Net;
using DnsClient;
using EmailVerifier.Models;

namespace EmailVerifier.Verify;

public class DnsValidator : IValidator
{
    private const int MaxCapacity = 30;
    private readonly Dictionary<string, int> _validDomains = new(MaxCapacity);

    public List<Pair<string, string>> Verify(ArrayList data, object? sender, int max)
    {
        var toBeRemoved = new List<Pair<string, string>>();
        var index = 1.0;
        foreach (Pair<string, string> pair in data)
        {
            if ((sender as BackgroundWorker)?.CancellationPending == true) throw new UserCancelException();

            var domain = pair.First.Substring(pair.First.IndexOf('@') + 1);
            if (_validDomains.ContainsKey(domain))
            {
                _validDomains[domain]++;
            }
            else
            {
                var dnsServers = new[] { IPAddress.Parse("8.8.8.8"), IPAddress.Parse("8.8.4.4") };
                var dnsLookup = new LookupClient(dnsServers);
                try
                {
                    var mxResult = dnsLookup.Query(domain, QueryType.MX).Answers.Any();
                    var aResult = dnsLookup.Query(domain, QueryType.A).Answers.Any();
                    var aaaResult = dnsLookup.Query(domain, QueryType.AAAA).Answers.Any();
                    if (!mxResult && !aResult && !aaaResult)
                    {
                        toBeRemoved.Add(pair);
                    }
                    else
                    {
                        if (_validDomains.Count == MaxCapacity) RemoveLft();

                        _validDomains.Add(domain, 1);
                    }
                }
                catch (TimeoutException)
                {
                }
                catch (DnsResponseException)
                {
                    toBeRemoved.Add(pair);
                }
            }

            var percent = (int)(index / max * 100);
            (sender as BackgroundWorker)?.ReportProgress(percent);
            index++;
        }

        return toBeRemoved;
    }

    private void RemoveLft()
    {
        var least = new KeyValuePair<string, int>("", int.MaxValue);
        foreach (var pair in _validDomains)
            if (pair.Value < least.Value)
                least = pair;
        _validDomains.Remove(least.Key);
    }
}