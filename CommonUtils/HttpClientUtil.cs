using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CommonUtils
{
    public  class HttpClientUtil
    {
        private readonly Uri _baseUrl;
      
        public HttpClientUtil(string baseUrl) =>  this._baseUrl= new Uri( baseUrl);
     
        public async Task<T> PostWithJson<T>(string path, object value,IDictionary<string,string> headers=null, IDictionary<string, string> cookie = null)
        {
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = _baseUrl })
            {
                if (cookie != null)
                    cookie.ToList().ForEach(k => cookieContainer.Add(_baseUrl, new Cookie(k.Key, k.Value)));

                HttpContent content = new StringContent(JsonConvert.SerializeObject(value));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                if (headers != null)
                    headers.ToList().ForEach(x => client.DefaultRequestHeaders.Add(x.Key, x.Value));
                var httpResponse = await client.PostAsync(path, content);
                httpResponse.EnsureSuccessStatusCode();//用来抛异常的
                string responseBody = await httpResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
        }

        


    }
}
