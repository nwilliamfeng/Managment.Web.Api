using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace EM.Management.Web
{
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public string ReasonPhrase { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, this.ReasonPhrase);
        }
    }

    /// <summary>
    /// 自定义认证
    /// </summary>
    public class AuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public virtual bool AllowMultiple
        {
            get { return false; }
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var principal = await this.AuthenticateAsync(context.Request);
            if (principal == null)
            {
                context.Request.Headers.GetCookies().Clear();
                context.ErrorResult = new AuthenticationFailureResult("未授权请求", context.Request);
            }
            else
            {
                context.Principal = principal;
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        private Task<IPrincipal> AuthenticateAsync(HttpRequestMessage request)
        {
            return Task.Run<IPrincipal>(() =>
            {
                CookieHeaderValue tokenCookie = request.Headers.GetCookies("token").FirstOrDefault();
               
                if (tokenCookie == null || string.IsNullOrWhiteSpace(tokenCookie["token"].Value))
                {
                    return null;
                }

                string token = tokenCookie["token"].Value;

                //ClientDTO client = null;
                ////此处从Redis服务器中取出指定用户，各位可以根据需要自行更换
                //using (ICache cache = ObjectContainer.Current.Resolve<ICacheFactory>().CreateCache())
                //{
                //    client = cache.Get<ClientDTO>(RedisTables.CLIENT, mobile);
                //}
                ////验证用户合法性，如果合法，构建声明式安全主题权限模式并返回，若用户验证不通过返回空
                //if (client != null && string.Equals(token, Md5Helper.MD5(string.Format("{0}{1}", mobile, client.MsgCode), 32), StringComparison.Ordinal))
                //{
                //    IEnumerable<Claim> claims = new List<Claim>()
                //        {
                //            new Claim(ClaimTypes.Name, mobile)
                //        };
                //    var identity = new ClaimsIdentity("LoanCookie");
                //    identity.AddClaims(claims);
                //    return new ClaimsPrincipal(identity);
                //}




                var identity = new ClaimsIdentity("LoanCookie");
                ;
                identity.AddClaim(new Claim(ClaimTypes.Name, token));
                var result = new ClaimsPrincipal(identity);
                 return result;

            });
        }
    }
}