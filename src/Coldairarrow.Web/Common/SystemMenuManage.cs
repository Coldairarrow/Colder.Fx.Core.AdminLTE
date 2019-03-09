using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Business.Common;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Coldairarrow.Web
{
    /// <summary>
    /// 系统菜单管理
    /// </summary>
    public static class SystemMenuManage
    {
        #region 构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SystemMenuManage()
        {
            InitAllMenu();
        }

        #endregion

        #region 私有成员

        private static List<Menu> _allMenu { get; set; }
        private static void InitAllMenu()
        {
            Action<Menu, XElement> SetMenuProperty = (menu, element) =>
            {
                List<string> exceptProperties = new List<string> { "id", "IsShow", "targetType", "isHeader", "children", "_url" };
                menu.GetType().GetProperties().Where(x => !exceptProperties.Contains(x.Name)).ForEach(aProperty =>
                {
                    aProperty.SetValue(menu, element.Attribute(aProperty.Name)?.Value);
                });
            };

            string filePath = _configFile;
            XElement xe = XElement.Load(filePath);
            List<Menu> menus = new List<Menu>();
            xe.Elements("FirstMenu")?.ForEach(aElement1 =>
            {
                Menu newMenu1 = new Menu();
                menus.Add(newMenu1);
                SetMenuProperty(newMenu1, aElement1);
                newMenu1.children = new List<Menu>();
                aElement1.Elements("SecondMenu")?.ForEach(aElement2 =>
                {
                    Menu newMenu2 = new Menu();
                    newMenu1.children.Add(newMenu2);
                    SetMenuProperty(newMenu2, aElement2);
                    newMenu2.children = new List<Menu>();

                    aElement2.Elements("ThirdMenu")?.ForEach(aElement3 =>
                    {
                        Menu newMenu3 = new Menu();
                        newMenu2.children.Add(newMenu3);
                        SetMenuProperty(newMenu3, aElement3);
                        if (!newMenu3.url.IsNullOrEmpty())
                        {
                            newMenu3.url = GetUrl(newMenu3.url);
                        }
                    });
                });
            });

            if (GlobalSwitch.RunModel == RunModel.LocalTest)
            {
                Menu newMenu1_1 = new Menu
                {
                    text = "开发",
                    icon = "glyphicon glyphicon-console",
                    children = new List<Menu>()
                };
                menus.Add(newMenu1_1);
                Menu newMenu1_1_1 = new Menu
                {
                    text = "代码生成",
                    icon = "fa fa-circle-o",
                    url = GetUrl("~/Base_SysManage/RapidDevelopment/Index")
                };
                newMenu1_1.children.Add(newMenu1_1_1);

                Menu newMenu1_1_2 = new Menu
                {
                    text = "数据库连接管理",
                    icon = "fa fa-circle-o",
                    url = GetUrl("~/Base_SysManage/Base_DatabaseLink/Index")
                };
                newMenu1_1.children.Add(newMenu1_1_2);

                //Menu newMenu1_1_3 = new Menu
                //{
                //    text = "UEditor Demo",
                //    url = GetUrl("~/Demo/UMEditor")
                //};
                //newMenu1_1.children.Add(newMenu1_1_3);

                //Menu newMenu1_1_4 = new Menu
                //{
                //    text = "文件上传Demo",
                //    url = GetUrl("~/Demo/UploadFileIndex")
                //};
                //newMenu1_1.children.Add(newMenu1_1_4);
            }

            _allMenu = menus;
        }
        private static string _configFile { get => PathHelper.GetAbsolutePath("~/Config/SystemMenu.config"); }
        public static string GetUrl(string virtualUrl) => PathHelper.GetUrl(virtualUrl);

        #endregion

        #region 外部接口

        /// <summary>
        /// 获取系统所有菜单
        /// </summary>
        /// <returns></returns>
        public static List<Menu> GetAllSysMenu()
        {
            return _allMenu.DeepClone();
        }

        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <returns></returns>
        public static List<Menu> GetOperatorMenu()
        {
            List<Menu> resList = GetAllSysMenu();

            if (Operator.IsAdmin())
                return resList;

            var userPermissions = PermissionManage.GetUserPermissionValues(Operator.UserId);
            RemoveNoPermission(resList, userPermissions);

            return resList;

            void RemoveNoPermission(List<Menu> menus, List<string> userPermissionValues)
            {
                for (int i = menus.Count - 1; i >= 0; i--)
                {
                    var theMenu = menus[i];
                    if (!theMenu.Permission.IsNullOrEmpty() && !userPermissions.Contains(theMenu.Permission))
                        menus.RemoveAt(i);
                    else if (theMenu.children?.Count > 0)
                    {
                        RemoveNoPermission(theMenu.children, userPermissions);
                        if (theMenu.children.Count == 0 && theMenu.url.IsNullOrEmpty())
                            menus.RemoveAt(i);
                    }
                }
            }
        }

        #endregion
    }

    #region 数据模型

    public class Menu
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string text { get; set; }
        public string icon { get; set; }
        public string url { get => SystemMenuManage.GetUrl(_url); set => _url = value; }
        public string _url { get; set; }
        public string Permission { get; set; }
        public bool IsShow { get; set; } = true;
        public string targetType { get; } = "iframe-tab";
        public bool isHeader { get; } = false;
        public List<Menu> children { get; set; }
    }

    #endregion
}