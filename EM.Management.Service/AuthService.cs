using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcomm;
using System.Configuration;
 
namespace EM.Management.Service
{
    public class AuthService : Microcomm.Security.ITokenIdentify
    {
        public async Task<JsonResultData<bool>> Validate(string accessToken)
        {
            var baseUrl = ConfigurationManager.AppSettings["innerAuthBaseUrl"];
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                ["Authorization"] = accessToken,
            };

            var result = await new HttpClientUtil(baseUrl).Get<JsonResultData<bool>>("/api/innerauth/vertify",headers);
            return result;

        }
    }
}
