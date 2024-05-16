using System.Net;
using System.Net.Sockets;

namespace Timesheet.Api.Repositories.Utils;

public static class UserUtilities
{
    public static string GetIPAndMacAddresses()
    {
        string strHostName = Dns.GetHostName();
        string ips = string.Empty;
        var host = Dns.GetHostEntry(strHostName);
        IPAddress[] addr = host.AddressList;
        string mac = addr[addr.Length - 1].MapToIPv4().ToString();
        foreach (var ip in addr)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ips = ip.ToString();
            }
        }

        return $"{ips}, {mac}";
    }
}
