using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace EM.Management.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Press any Command Number and return...");
                Console.WriteLine("1. Auth");
                Console.WriteLine("2. JWT");
                var key = int.Parse(Console.ReadLine());
                switch (key)
                {
                    case 1:
                        AuthTest.Init();
                        break;
                    case 2:
                        JwtTest.Init();
                        break;
                }
             
            }
        }

       
    }
}
