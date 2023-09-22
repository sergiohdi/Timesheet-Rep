using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Timesheet.Api.Repositories.Utils
{
    public static class UserUtilities
    {
        //public T SetUserAudit<T>(this T entity, HttpContext context, bool isCreateAction)
        //{

        //}

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
}
