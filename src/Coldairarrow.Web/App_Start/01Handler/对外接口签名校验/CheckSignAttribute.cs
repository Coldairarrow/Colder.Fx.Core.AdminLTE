using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Web
{
    /// <summary>
    /// 校验签名
    /// </summary>
    public class CheckSignAttribute : Attribute, IActionFilter
    {
        private CheckSignBusiness _checkSignBusiness { get; } = new CheckSignBusiness();

        /// <summary>
        /// Action执行之前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //若为本地测试，则不需要校验
            if (GlobalSwitch.RunModel == RunModel.LocalTest)
            {
                return;
            }

            //判断是否需要签名
            List<string> attrList = FilterHelper.GetFilterList(filterContext);
            bool needSign = attrList.Contains(typeof(CheckSignAttribute).FullName) && !attrList.Contains(typeof(IgnoreSignAttribute).FullName);

            //不需要签名
            if (!needSign)
                return;

            //需要签名
            if (!_checkSignBusiness.IsSecurity(filterContext.HttpContext))
            {
                AjaxResult res = new AjaxResult
                {
                    Msg = "签名校验失败！拒绝访问！",
                    Success = false
                };
                filterContext.Result = new ContentResult() { Content = res.ToJson() };
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