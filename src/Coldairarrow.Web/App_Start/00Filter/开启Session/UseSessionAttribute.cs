using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Web
{
    /// <summary>
    /// 使用Session
    /// </summary>
    public class UseSessionAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var requestCookies = context.HttpContext.Request.Cookies.GetPropertyValue("Store") as Dictionary<string, string>;
            if (requestCookies == null)
            {
                requestCookies = new Dictionary<string, string>();
                context.HttpContext.Request.Cookies.SetPropertyValue("Store", requestCookies);
            }

            var sessionCookie = context.HttpContext.Request.Cookies[SessionHelper.SessionCookieName];
            if (sessionCookie.IsNullOrEmpty())
            {
                string sessionId = Guid.NewGuid().ToString();
                requestCookies.Add(SessionHelper.SessionCookieName, sessionId);
                context.HttpContext.Response.Cookies.Append(SessionHelper.SessionCookieName, sessionId, new CookieOptions { Expires = DateTime.MaxValue, SameSite = SameSiteMode.None });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
