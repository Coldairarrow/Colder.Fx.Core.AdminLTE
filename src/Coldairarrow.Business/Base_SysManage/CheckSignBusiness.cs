using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coldairarrow.Business.Base_SysManage
{
    public class CheckSignBusiness : BaseBusiness<Base_AppSecret>
    {
        /// <summary>
        /// 判断是否有权限操作接口
        /// </summary>
        /// <returns></returns>
        public bool IsSecurity(HttpContext context)
        {
            try
            {
                var request = context.Request;
                var allRequestParams = GetAllRequestParams(context);
                string appId = allRequestParams["appId"]?.ToString();
                string appSecret = GetAppSecret(appId);

                return CheckSign(allRequestParams, appSecret);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取应用密钥
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <returns></returns>
        public string GetAppSecret(string appId)
        {
            return GetIQueryable().Where(x => x.AppId == appId).FirstOrDefault()?.AppSecret;
        }

        /// <summary>
        /// 获取所有请求的参数（包括get参数和post参数）
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <returns></returns>
        private Dictionary<string, object> GetAllRequestParams(HttpContext context)
        {
            return HttpHelper.GetAllRequestParams(context);
        }

        /// <summary>
        /// 检验签名是否有效
        /// </summary>
        /// <param name="allRequestParames">所有的请求参数</param>
        /// <param name="appSecret">应用密钥</param>
        /// <returns></returns>
        private bool CheckSign(Dictionary<string, object> allRequestParames, string appSecret)
        {
            try
            {
                //检验签名是否过期
                DateTime now = DateTime.Now.ToCstTime();
                DateTime requestTime = Convert.ToDateTime(allRequestParames["time"]?.ToString());
                if (requestTime < now.AddMinutes(-5) || requestTime > now.AddMinutes(5))
                    return false;

                //检验签名是否有效
                string oldSign = allRequestParames["sign"]?.ToString();
                Dictionary<string, object> parames = new Dictionary<string, object>();
                foreach (var aParam in allRequestParames)
                {
                    parames.Add(aParam.Key, aParam.Value);
                }

                parames.Remove("sign");
                string newSign = BuildSign(parames, appSecret);

                return oldSign == newSign;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 构建安全的请求参数(默认签名规则)
        /// </summary>
        /// <param name="businessParams">业务参数（不包含校验参数）</param>
        /// <param name="appId">应用Id</param>
        /// <param name="appSecret">应用密钥</param>
        public static Dictionary<string, object> BuildSafeHttpParam(Dictionary<string, object> businessParams, string appId, string appSecret)
        {
            Dictionary<string, object> requestParames = new Dictionary<string, object>();

            if (businessParams != null)
            {
                foreach (var aParam in businessParams)
                {
                    requestParames.Add(aParam.Key, aParam.Value);
                }
            }

            requestParames.Add("time", DateTime.Now.ToCstTime().ToString("yyyy-MM-dd HH:mm:ss"));
            requestParames.Add("appId", appId);

            string sign = BuildSign(requestParames, appSecret);
            requestParames.Add("sign", sign);

            return requestParames;
        }

        /// <summary>
        /// 构造签名
        /// </summary>
        /// <param name="needParames">需要的参数（不包括sign）</param>
        /// <param name="appSecret">应用密钥</param>
        /// <returns></returns>
        private static string BuildSign(Dictionary<string, object> needParames, string appSecret)
        {
            var sortedParams = new SortedDictionary<string, object>(new AsciiComparer());
            foreach (var aParam in needParames)
            {
                sortedParams.Add(aParam.Key, aParam.Value);
            }

            StringBuilder signBuilder = new StringBuilder();
            foreach (var aParam in sortedParams)
            {
                var value = aParam.IsNullOrEmpty() ? string.Empty : aParam.Value.ToString();
                signBuilder.Append($@"{aParam.Key}{value}");
            }
            signBuilder.Append(appSecret);
            string sign = signBuilder.ToString().ToMD5String().ToUpper();

            return sign;
        }

        /// <summary>
        /// 基于ASCII码排序规则的String比较器
        /// </summary>
        class AsciiComparer : IComparer<string>
        {
            public int Compare(string a, string b)
            {
                if (a == b)
                    return 0;
                else if (string.IsNullOrEmpty(a))
                    return -1;
                else if (string.IsNullOrEmpty(b))
                    return 1;
                if (a.Length <= b.Length)
                {
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a[i] < b[i])
                            return -1;
                        else if (a[i] > b[i])
                            return 1;
                        else
                            continue;
                    }
                    return a.Length == b.Length ? 0 : -1;
                }
                else
                {
                    for (int i = 0; i < b.Length; i++)
                    {
                        if (a[i] < b[i])
                            return -1;
                        else if (a[i] > b[i])
                            return 1;
                        else
                            continue;
                    }
                    return 1;
                }
            }
        }
    }
}

