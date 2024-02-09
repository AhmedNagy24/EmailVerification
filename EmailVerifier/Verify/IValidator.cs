using System.Collections;
using EmailVerifier.Models;

namespace EmailVerifier.Verify;

public interface IValidator
{
    public List<Pair<string, string>> Verify(ArrayList data, object? sender, int max);
}