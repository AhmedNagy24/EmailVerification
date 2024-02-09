using System.Collections;
using EmailVerifier.Models;

namespace EmailVerifier.Verify;

public class VerifyManager(IValidator validator)
{
    public IValidator Validator { get; set; } = validator;

    public List<Pair<string, string>> Verify(ArrayList data, object? sender, int max)
    {
        return Validator.Verify(data, sender, max);
    }
}