using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Web.Areas.Base_SysManage.Controllers
{
    [Area("Base_SysManage")]
    public class CityController : BaseController
    {
        static CityController()
        {
            string jsonStr = System.IO.File.ReadAllText(PathHelper.GetAbsolutePath("~/Config/Province.json"));
            _countryRegin = jsonStr.ToObject<CountryRegin>();
        }
        private static CountryRegin _countryRegin { get; } = null;

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="q">省份名</param>
        /// <returns></returns>
        public ActionResult GetProvinceList(string q)
        {
            var predicate = LinqHelper.True<ProvinceListItem>();
            if (!q.IsNullOrEmpty())
                predicate = predicate.And(x => x.ProName.Contains(q));

            return Content(_countryRegin.provinceList.Where(predicate.Compile()).ToJson());
        }

        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="q">城市名</param>
        /// <param name="proId">省份Id</param>
        /// <returns></returns>
        public ActionResult GetCityList(string q, string proId)
        {
            var predicate = LinqHelper.True<CityListItem>();
            if (!q.IsNullOrEmpty())
                predicate = predicate.And(x => x.CityName.Contains(q));
            if (!proId.IsNullOrEmpty())
                predicate = predicate.And(x => x.ProID.ToString() == proId);

            return Content(_countryRegin.cityList.Where(predicate.Compile()).ToJson());
        }

        /// <summary>
        /// 获取县城列表
        /// </summary>
        /// <param name="q">县城名</param>
        /// <param name="cityId">城市Id</param>
        /// <returns></returns>
        public ActionResult GetCountyList(string q, string cityId)
        {
            var predicate = LinqHelper.True<CountyListItem>();
            if (!q.IsNullOrEmpty())
                predicate = predicate.And(x => x.DisName.Contains(q));
            if (!cityId.IsNullOrEmpty())
                predicate = predicate.And(x => x.CityID.ToString() == cityId);

            return Content(_countryRegin.countyList.Where(predicate.Compile()).ToJson());
        }

        #endregion

        #region 提交数据

        #endregion

        #region 数据模型

        public class ProvinceListItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int ProID { get; set; }
            /// <summary>
            /// 北京市
            /// </summary>
            public string ProName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int ProSort { get; set; }
            /// <summary>
            /// 直辖市
            /// </summary>
            public string ProRemark { get; set; }
        }

        public class CityListItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int CityID { get; set; }
            /// <summary>
            /// 北京市
            /// </summary>
            public string CityName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int ProID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int CitySort { get; set; }
        }

        public class CountyListItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 县城名
            /// </summary>
            public string DisName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int CityID { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string DisSort { get; set; }
        }

        public class CountryRegin
        {
            /// <summary>
            /// 
            /// </summary>
            public List<ProvinceListItem> provinceList { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<CityListItem> cityList { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<CountyListItem> countyList { get; set; }
        }

        #endregion
    }
}