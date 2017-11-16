using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
namespace qPing
{
    class Program
    {
        static void Main(string[] args)
        {
            PingReply reply;
            IPAddress address;
            Int32 pingCount = 4;
            Int32 addressCount = 4;
            Ping pingSender = new Ping();

            if (args.Length < 1)
            {
                ShowUsage("Too few parameters");
                return;
            }

            if (args.Length > 3)
            {
                ShowUsage("Too many parameters");
                return;
            }

            if (! IPAddress.TryParse(args[0], out address))
            {
                ShowUsage("Invalid IP address");
                return;
            }

            if (args.Length >= 2 && ! Int32.TryParse(args[1], out addressCount))
            {
                ShowUsage("Invalid address-count - " + args[1]);
                return;
            }

            if (args.Length >= 3 && !Int32.TryParse(args[2], out pingCount))
            {
                ShowUsage("Invalid print-count - " + args[2]);
                return;
            }

            for (int i = 0; i < addressCount; i++)
            {
                Console.WriteLine(String.Format("\nPinging {0} ...", address.ToString()));
                for (int n = 0; n < pingCount; n++)
                {
                    reply = pingSender.Send(address);
                    if (reply.Status == IPStatus.Success)
                        Console.WriteLine(String.Format("\tReply from {0}: bytes={1} TTL={2} reply-time={3}ms ", reply.Address, reply.Buffer.Length, reply.Options.Ttl, reply.RoundtripTime));
                    else
                        Console.WriteLine(String.Format("\t{0}", reply.Status));
                }
                IPAddress.TryParse(GetNextIpAddress(address.ToString(), 1), out address);
            }
            ShowExitMessage();
        }

        static string GetNextIpAddress(string ipAddress, uint increment)
        {
            byte[] addressBytes = IPAddress.Parse(ipAddress).GetAddressBytes().Reverse().ToArray();
            uint ipAsUint = BitConverter.ToUInt32(addressBytes, 0);
            var nextAddress = BitConverter.GetBytes(ipAsUint + increment);
            return String.Join(".", nextAddress.Reverse());
        }

        static void ShowUsage(string error)
        {
            Console.WriteLine(String.Format("\nqping: {0}!\n", error));
            Console.WriteLine("Usage:");
            Console.WriteLine("\tqping ipaddress [range-count [ping-count]]\n");
            Console.WriteLine("\tipaddress      - valid IPV4 address");
            Console.WriteLine("\taddress-count  - (optional) number of addresses to ping (default: 4)");
            Console.WriteLine("\tping-count     - (optional)number of pings to send to each address (default: 4)\n");
            Console.WriteLine("\tFor example, the following will ping 89.150.31.60, then 89.150.31.61, then 89.150.31.62 - twice each\n");
            Console.WriteLine("\tqping 89.150.31.60 3 2\n");

            ShowExitMessage();
        }

        static void ShowExitMessage()
        {
            Console.WriteLine("\nqPing! - a simple, handy utility by Andy Newlands \x01");
        }
    }
}
