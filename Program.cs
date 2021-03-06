﻿/*
This program was developed by Christensen, Martin Smed; Jacobsen, Daniel van Dijk; Romby, Josephine; Varchev, Aleksander Iliyanov
in relation to the RAWDATA course at Roskilde University.
 */
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RAWDATA_Assignment_3
{
    class ServerProgram
    {
        static void Main(string[] args)
        {
            var server = new TcpListener(IPAddress.Loopback, 5000);
            server.Start();
            Console.WriteLine("Server started!");

            while (true)
            {
                var client = server.AcceptTcpClient();
                Console.WriteLine("Accepted client!");

                var stream = client.GetStream();


                var msg = Read(client, stream);

                Console.WriteLine($"Message from client {msg}");

                //Method to create and return new JSON

                var data = Encoding.UTF8.GetBytes(msg.ToUpper());
                stream.Write(data);

            }
        }

        private static string Read(TcpClient client, NetworkStream stream)
        {
            byte[] data = new byte[client.ReceiveBufferSize];
            var cnt = stream.Read(data);
            //Creating unnamed type from JSON
            //var incoming = new {method, path, date, body};
            var msg = Encoding.UTF8.GetString(data, 0, cnt);
            return msg;
        }
    }
}
