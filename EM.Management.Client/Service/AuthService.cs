using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;
using System.Net;
using CommonUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EM.Management.Client
{
    public class AuthService
    {
        private const string LOGIN_URL = "auth/login";

    
        

        private  HttpClientUtil _httpClientUtil;

        public AuthService()
        {
            _httpClientUtil = new HttpClientUtil(ConfigurationManager.AppSettings["serviceHost"]);
        }
         


        public async Task<LoginResult> Login(string userName, string password)
        {
            JObject para = new JObject();
            para["userId"] = userName;
            para["password"] = password;
            var result = await this._httpClientUtil.PostWithJson<JsonResultData< LoginResult>>("api/auth/login",para);
            if (result.StatusCode != 1)
                throw new ArgumentException(result.Message);
            return result.Data;
        }


        public async Task<bool> Logout(string userName,string accessToken)
        {
            JObject para = new JObject();
            para["userId"] = userName;
            para["accessToken"] = accessToken;
            var dic = new Dictionary<string, string>
            {
                ["userId"] =Uri.EscapeDataString( userName),
                ["accessToken"] =Uri.EscapeDataString( accessToken),
            };

            var result= await this._httpClientUtil.PostWithJson<JsonResultData<bool>>("api/auth/logout", para,dic);
            return result.Data;
        }
     
       
    }
}
