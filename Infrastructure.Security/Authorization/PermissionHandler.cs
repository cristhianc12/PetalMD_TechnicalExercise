using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security.Authorization
{
    public class PermissionHandler : IAuthorizationHandler
    {
        IAuthService _authService;
        public PermissionHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                //string idUser = context.User.Claims
                string controller = ((ControllerActionDescriptor)((ActionContext)context.Resource).ActionDescriptor).ControllerName;
                string action = ((ControllerActionDescriptor)((ActionContext)context.Resource).ActionDescriptor).ActionName;
                string userId = context.User.Identity.Name;
                //string userId = context.User.Claims.First(claim => claim.Type == "NameId").Value;
                string token = context.User.Claims.First(claim => claim.Type == "jti").Value;
                string userAgent = string.IsNullOrEmpty(((ActionContext)context.Resource).HttpContext.Request.Headers["User-Agent"]) ? string.Empty : ((ActionContext)context.Resource).HttpContext.Request.Headers["User-Agent"].ToString();
                string RemoteIpAddress = ((ActionContext)context.Resource).HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
                string LocalIpAddress = ((ActionContext)context.Resource).HttpContext.Request.HttpContext.Connection.LocalIpAddress.ToString();
                if (!_authService.Authorized(token, userId, controller, action, RemoteIpAddress, LocalIpAddress, userAgent))
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
