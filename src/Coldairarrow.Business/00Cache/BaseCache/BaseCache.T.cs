using Coldairarrow.Util;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Business.Cache
{
    public class BaseCache<T> : IBaseCache<T> where T : class
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cacheModuleKey">缓存模块键值</param>
        /// <param name="getDataFunc">根据主键获取数据的具体方法</param>
        public BaseCache(string cacheModuleKey, Func<string, T> getDataFunc)
        {
            _moduleKey = cacheModuleKey;
            _getDataFunc = getDataFunc;
        }

        #endregion

        #region 私有成员

        private Func<string, T> _getDataFunc { get; set; }
        public string _moduleKey { get; set; }


        #endregion

        #region 外部接口

        public string BuildKey(string idKey)
        {
            return $"{GlobalSwitch.ProjectName}_Cache_{_moduleKey}_{idKey}";
        }

        public T GetCache(string idKey)
        {
            if (idKey.IsNullOrEmpty())
                return null;

            string cacheKey = BuildKey(idKey);
            var cache = CacheHelper.Cache.GetCache<T>(cacheKey);
            if (cache == null)
            {
                cache = _getDataFunc(idKey);
                if (cache != null)
                    CacheHelper.Cache.SetCache(cacheKey, cache);
            }

            return cache;
        }

        public void UpdateCache(string idKey)
        {
            CacheHelper.Cache.RemoveCache(BuildKey(idKey));
        }

        public void UpdateCache(List<string> idKeys)
        {
            idKeys.ForEach(x => UpdateCache(x));
        }

        #endregion
    }
}
