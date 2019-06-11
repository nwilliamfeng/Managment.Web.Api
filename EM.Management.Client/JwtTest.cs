using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EM.Management.Client
{
    public static class JwtTest
    {
        private const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
        private static string token = null;


        public static void Init()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("press any auth command number and return to execute...");
                    Console.WriteLine("1. Encypt");
                    Console.WriteLine("2. Decypt");
                    Console.WriteLine("0. Exit");
                    var num = int.Parse(Console.ReadLine());
                    switch (num)
                    {
                        case 1:
                            Encypt();
                            break;
                        case 2:
                            Decypt();
                            break;
                        case 0:

                            break;
                    }
                    if (num == 0)
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public static void Encypt()
        {
            token = new JwtBuilder()
      .WithAlgorithm(new HMACSHA256Algorithm())
      .WithSecret(secret)
      .AddClaim("exp", DateTimeOffset.UtcNow.AddSeconds(10).ToUnixTimeSeconds())
      .AddClaim("claim2", "claim2-value")
      .Build();

            Console.WriteLine(token);

        }

        public static void Decypt()
        {
            var payload = new JwtBuilder()
        .WithSecret(secret)
        .MustVerifySignature()
        .Decode(token);
            Console.WriteLine(payload);
        }
    }
}
