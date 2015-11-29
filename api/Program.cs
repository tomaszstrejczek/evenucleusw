using System;
using Microsoft.Owin.Hosting;

namespace ts.api
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://+:8070/api";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }
    }
}
