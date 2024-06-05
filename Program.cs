﻿using ChatApp;
using ChatCommon.Models;
using ChatCommon.Abstractions;
using ChatDB;
using System.Net;




namespace Seminar18
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                var s = new Server<IPEndPoint>(new UdpMessageSourceServer());
                await s.Start();
            }
            if (args.Length == 1)
            {
                var c = new Client<IPEndPoint>(new UdpMessageSourceClient(), args[0]);
                await c.Start();
            }
            else
            {
                Console.WriteLine("Для запуска сервера введите ник-нейм как параметр запуска приложения");
                Console.WriteLine("Для запуска клиента введите ник-нейм и IP сервера как параметры запуска приложения");
            }
            Console.ReadKey(true);
        }
    } 
}