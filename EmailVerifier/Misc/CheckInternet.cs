using System.Net.NetworkInformation;

namespace EmailVerifier.Misc;

public static class CheckInternet
{
    public static bool IsInternetAvailable()
    {
        try
        {
            var ping = new Ping();
            var reply = ping.Send("google.com", 3000);
            if (reply.Status != IPStatus.Success) return false;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}