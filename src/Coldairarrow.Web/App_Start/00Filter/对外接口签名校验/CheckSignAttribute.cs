using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Web
{
    /*
==== 签名算法 ====

appId:AppAdmin

appSecret:oEt6sgjgPGeA5wFX

每个接口必须参数

| appId | string | 应用Id |
| time | string | 当前时间，格式为：2017-01-01 23:00:00 |
| guid | string | GUID字符串,作为请求唯一标志,防止重复请求 |
| sign| string | 签名,签名算法如下 |

签名算法示例：

令:

appId=xxx

appSecret=xxx

time=2017-01-01 23:00:00

1.对除签名外的所有请求参数按key做升序排列(字符串ASCII排序)

例如：有c=3,b=2,a=1 三个业务参数，另需要加上校验签名参数appId和time， 按key排序后为：a=1，appId=xxx，b=2，c=3，time=2017-01-01 23:00:00。

2 把参数名和参数值连接成字符串，得到拼装字符：a1appIdxxxb2c3guid33f01f62-8f82-42a6-a01f-97e96cf035abtime2017-01-01 23:00:00

3 用申请到的appSecret连接到接拼装字符串尾部，然后进行32位MD5加密，最后将到得MD5加密摘要转化成大写,即得到签名sign

示例：拼接字符串为a1appIdxxxb2c3guid33f01f62-8f82-42a6-a01f-97e96cf035abtime2017-01-01 23:00:00,appSecret为xxx,则sign=6318255C8C1453EAD251E944DF1BD28C     
    */
    /// <summary>
    /// 校验签名、十分严格
    /// 防抵赖、防伪造、防重复调用
    /// </summary>
    public class CheckSignAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// Action执行之前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ICheckSignBusiness checkSignBusiness = AutofacHelper.GetService<ICheckSignBusiness>();

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

            //是否安全
            var isSuccess = checkSignBusiness.IsSecurity(filterContext.HttpContext);
            if (isSuccess)
                return;
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