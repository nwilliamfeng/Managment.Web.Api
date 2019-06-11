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
using Autofac;
using EM.Management.Service;

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

        private async Task<IPrincipal> AuthenticateAsync(HttpRequestMessage request)
        {
//#if DEBUG
       //     var testIdentity = new ClaimsIdentity("LoanCookie");
       //     testIdentity.AddClaim(new Claim(ClaimTypes.Name, "test"));
       //     return new ClaimsPrincipal(testIdentity);
//#endif

           
    
            CookieHeaderValue tokenCookie = request.Headers.GetCookies("accessToken").FirstOrDefault();
            CookieHeaderValue idCookie = request.Headers.GetCookies("userId").FirstOrDefault();
            if (tokenCookie == null || string.IsNullOrWhiteSpace(tokenCookie["accessToken"].Value) || idCookie == null || string.IsNullOrWhiteSpace(idCookie["userId"].Value))
                return null;

            var token = tokenCookie["accessToken"].Value; //注意，这里从cookie里取数据会自动进行反转义，也就是.net客户端传值时候需要先进行转义
            var userId = idCookie["userId"].Value;

            //验证用户合法性，如果合法，构建声明式安全主题权限模式并返回，若用户验证不通过返回空
            var isValid = false;
            using (var scope = AutofacWebapiConfig.Container.BeginLifetimeScope())
            {
                var authService = scope.Resolve<IAuthService>();
                isValid = (await authService.Validate(userId, token)).Data;
            }
            if (isValid)
            {
                var identity = new ClaimsIdentity("LoanCookie");
                identity.AddClaim(new Claim(ClaimTypes.Name, token));
                var result = new ClaimsPrincipal(identity);
                return result;
            }
            return null;
        }

        private string GetTokenFromRequest(HttpRequestMessage request)
        {
            if(request.Headers.Contains("accessToken"))
        }
    }
}