using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Management.Model;
using Newtonsoft.Json;

namespace EM.Management.Client
{
    public static class AuthTest
    {
        public  static void Init()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("press any auth command number and return to execute...");
                    Console.WriteLine("1. Login");
                    Console.WriteLine("2. Logout");
                    Console.WriteLine("0. Exit");
                    var num = int.Parse(Console.ReadLine());
                    switch (num)
                    {
                        case 1:
                            Console.WriteLine("ready to execute login...");
                            Console.Write("username:");
                            var userName = Console.ReadLine();
                            Console.Write("password:");
                            var password = Console.ReadLine();
                            ExecuteLogin(userName, password);
                            break;
                        case 2:
                            Console.WriteLine("ready to execute logout...");
                            ExecuteLogout();
                            break;
                        case 0:

                            break;
                    }
                    if (num == 0)
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static LoginResult currLoginResult;

        public static async void ExecuteLogin(string name ,string password)
        {
            var auth = new AuthService();
              currLoginResult = await auth.Login(name, password);
           
            Console.WriteLine(JsonConvert.SerializeObject(currLoginResult));
        }

        public static async void ExecuteLogout()
        {
            if (currLoginResult == null)
                Console.WriteLine("there is no logininfo,logout is fail...");
            var auth = new AuthService();
            var result = await auth.Logout(currLoginResult.AccessToken);
            
            Console.WriteLine("注销"+(result?"成功":"失败"));
        }
    }
}
