using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Protocol
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient udpServer = new UdpClient(11000);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                var remoteEP = new IPEndPoint(IPAddress.Any, 11000);
                var data = udpServer.Receive(ref remoteEP); // listen on port 11000
                Console.WriteLine($"    User {remoteEP} {Encoding.ASCII.GetString(data)}");


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("My: ");
                var nMess = Console.ReadLine();
                var nMessBytes = Encoding.ASCII.GetBytes(nMess);
                udpServer.Send(nMessBytes, nMessBytes.Length, remoteEP); // reply back
            }

        }
    }
}
