using Coldairarrow.Business.Common;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Web
{
    /// <summary>
    /// 校验登录
    /// </summary>
    public class CheckLoginAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// Action执行之前执行
        /// </summary>
        /// <param name="filterContext">过滤器上下文</param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            try
            {
                //若为本地测试，则不需要登录
                if (GlobalSwitch.RunModel == RunModel.LocalTest)
                {
                    return;
                }
                //判断是否需要登录
                List<string> attrList = FilterHelper.GetFilterList(filterContext);
                bool needLogin = attrList.Contains(typeof(CheckLoginAttribute).FullName) && !attrList.Contains(typeof(IgnoreLoginAttribute).FullName);

                //转到登录
                if (needLogin && !Operator.Logged())
                {
                    RedirectToLogin();
                }
            }
            catch (Exception ex)
            {
                BusHelper.HandleException(ex);
                RedirectToLogin();
            }

            void RedirectToLogin()
            {
                if (request.IsAjaxRequest())
                {
                    filterContext.Result = new ContentResult
                    {
                        Content = new AjaxResult { Success = false, ErrorCode = 1, Msg = "未登录" }.ToJson(),
                        ContentType = "application/json;charset=UTF-8"
                    };
                }
                else
                {
                    UrlHelper urlHelper = new UrlHelper(filterContext);
                    string loginUrl = urlHelper.Content("~/Home/Login");
                    string script = $@"    
<html>
    <script>
        top.location.href = '{loginUrl}';
    </script>
</html>
";
                    filterContext.Result = new ContentResult { Content = script, ContentType = "text/html" };
                }
            }
        }

        /// <summary>
        /// Action执行完毕之后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}