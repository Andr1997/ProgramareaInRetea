using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDP_Protocol_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000); // endpoint where server is listening
            client.Connect(ep);

            while (true)
            {
                // send data

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("My: ");
                var mes = Console.ReadLine();
                var messBytes = Encoding.ASCII.GetBytes(mes);
                client.Send(messBytes, messBytes.Length);

                // then receive data
                var receivedData = client.Receive(ref ep);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"    User {ep} {Encoding.ASCII.GetString(receivedData)}");
            }

            Console.Read();
        }
    }
}
