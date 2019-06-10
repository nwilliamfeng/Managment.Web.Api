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
            Console.ReadLine();
            new AuthService().Logout( );
            Console.ReadLine();
        }

       
    }
}
