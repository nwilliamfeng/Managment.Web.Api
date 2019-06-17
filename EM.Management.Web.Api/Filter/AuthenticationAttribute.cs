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
using Microcomm.Web.Http.Autofac;

namespace EM.Management.Web
{
    //public class AuthenticationFailureResult : IHttpActionResult
    //{
    //    public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
    //    {
    //        ReasonPhrase = reasonPhrase;
    //        Request = request;
    //    }

    //    public string ReasonPhrase { get; private set; }

    //    public HttpRequestMessage Request { get; private set; }

    //    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(Execute());
    //    }

    //    private HttpResponseMessage Execute()
    //    {
    //        return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, this.ReasonPhrase);
    //    }
    //}

    ///// <summary>
    ///// 自定义认证
    ///// </summary>
    //public class AuthenticationAttribute : Attribute, IAuthenticationFilter
    //{
    //    public virtual bool AllowMultiple
    //    {
    //        get { return false; }
    //    }

    //    public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
    //    {
    //        var pt = await this.AuthenticateAsync(context.Request);
    //        if (pt.Item1 == null)
    //        {
    //            context.Request.Headers.GetCookies().Clear();
    //            //  context.ErrorResult = new AuthenticationFailureResult("未授权请求", context.Request);
    //            string msg = null;
    //            switch (pt.Item2)
    //            {
    //                case StatusCodes.TOKEN_EXPIRE:
    //                    msg = "令牌已过期";
    //                    break;
    //                case StatusCodes.TOKEN_INVALID:
    //                    msg = "非法的令牌";
    //                    break;
    //                case StatusCodes.TOKEN_NOT_FOUND:
    //                    msg = "令牌不存在";
    //                    break;
    //                default:
    //                    break;
    //            }
    //            context.ErrorResult = context.Request.JsonResult(false.ToJson(msg==null? "授权失败":msg).SetStatusCode(pt.Item2));
    //        }
    //        else
    //        {
    //            context.Principal = pt.Item1;
    //        }
    //    }

    //    public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(0);
    //    }
 
    //    private async Task<Tuple< IPrincipal,int>> AuthenticateAsync(HttpRequestMessage request)
    //    {
    //        int statusCode = 0;
    //        if(request.Headers.Authorization==null)
    //            return new Tuple<IPrincipal, int>(null, StatusCodes.TOKEN_NOT_FOUND);
    //        var token = request.Headers.Authorization.Scheme;
    //        if (string.IsNullOrEmpty(token))
    //            return new Tuple<IPrincipal, int>(null,StatusCodes.TOKEN_NOT_FOUND);
    //        //验证用户合法性，如果合法，构建声明式安全主题权限模式并返回，若用户验证不通过返回空
    //        using (var scope = AutofacWebapiConfig.Container.BeginLifetimeScope())
    //        {
    //            var authService = scope.Resolve<IAuthService>();
    //            var result = await authService.Validate(token);
    //            statusCode = result.StatusCode;
    //        }
    //        if (statusCode==1) //成功
    //        {
    //            var identity = new ClaimsIdentity("LoanToken");
    //            identity.AddClaim(new Claim(ClaimTypes.Name, token));
    //            var result = new ClaimsPrincipal(identity);
    //            return new Tuple<IPrincipal, int>( result,statusCode);
    //        }
    //        return new Tuple<IPrincipal, int>(null,statusCode);
    //    }

        
    //}
}