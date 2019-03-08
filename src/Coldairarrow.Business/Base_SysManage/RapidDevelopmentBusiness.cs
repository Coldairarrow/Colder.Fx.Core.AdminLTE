using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Coldairarrow.Business.Base_SysManage
{
    public class RapidDevelopmentBusiness : BaseBusiness<Base_DatabaseLink>
    {
        static RapidDevelopmentBusiness()
        {
            _contentRootPath = AutofacHelper.GetService<IHostingEnvironment>().ContentRootPath;
        }

        #region 外部接口

        /// <summary>
        /// 获取所有数据库连接
        /// </summary>
        /// <returns></returns>
        public List<Base_DatabaseLink> GetAllDbLink()
        {
            return GetList();
        }

        /// <summary>
        /// 获取数据库所有表
        /// </summary>
        /// <param name="linkId">数据库连接Id</param>
        /// <returns></returns>
        public List<DbTableInfo> GetDbTableList(string linkId)
        {
            if (linkId.IsNullOrEmpty())
                return new List<DbTableInfo>();
            else
                return GetTheDbHelper(linkId).GetDbAllTables();
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="linkId">连接Id</param>
        /// <param name="areaName">区域名</param>
        /// <param name="tables">表列表</param>
        /// <param name="buildType">需要生成类型</param>
        public void BuildCode(string linkId, string areaName, string tables, string buildType)
        {
            //内部成员初始化
            _dbHelper = GetTheDbHelper(linkId);
            GetDbTableList(linkId).ForEach(aTable =>
            {
                _dbTableInfoDic.Add(aTable.TableName, aTable);
            });

            List<string> tableList = tables.ToList<string>();
            List<string> buildTypeList = buildType.ToList<string>();
            tableList.ForEach(aTable =>
            {
                var tableFieldInfo = _dbHelper.GetDbTableInfo(aTable);
                //实体层
                if (buildTypeList.Exists(x => x.ToLower() == "entity"))
                {
                    BuildEntity(tableFieldInfo, areaName, aTable);
                }
                //业务层
                if (buildTypeList.Exists(x => x.ToLower() == "business"))
                {
                    BuildBusiness(areaName, aTable);
                }
                //控制器
                if (buildTypeList.Exists(x => x.ToLower() == "controller"))
                {
                    BuildController(areaName, aTable);
                }
                //视图
                if (buildTypeList.Exists(x => x.ToLower() == "view"))
                {
                    BuildView(tableFieldInfo, areaName, aTable);
                }
            });
        }

        #endregion

        #region 私有成员

        /// <summary>
        /// 生成实体
        /// </summary>
        /// <param name="tableInfo">表字段信息</param>
        /// <param name="areaName">区域名</param>
        /// <param name="tableName">表名</param>
        private void BuildEntity(List<TableInfo> tableInfo, string areaName, string tableName)
        {
            string entityPath = _contentRootPath.Replace("Coldairarrow.Web", "Coldairarrow.Entity");
            string filePath = Path.Combine(entityPath, areaName, $"{tableName}.cs");
            string nameSpace = $@"Coldairarrow.Entity.{areaName}";

            _dbHelper.SaveEntityToFile(tableInfo, tableName, _dbTableInfoDic[tableName].Description, filePath, nameSpace);
        }

        /// <summary>
        /// 生成业务逻辑代码
        /// </summary>
        /// <param name="areaName">区域名</param>
        /// <param name="entityName">实体名</param>
        private void BuildBusiness(string areaName, string entityName)
        {
            string code =
$@"using Coldairarrow.Entity.{areaName};
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Coldairarrow.Business.{areaName}
{{
    public class {entityName}Business : BaseBusiness<{entityName}>
    {{
        #region 外部接口

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name=""condition"">查询类型</param>
        /// <param name=""keyword"">关键字</param>
        /// <returns></returns>
        public List<{entityName}> GetDataList(string condition, string keyword, Pagination pagination)
        {{
            var q = GetIQueryable();

            //模糊查询
            if (!condition.IsNullOrEmpty() && !keyword.IsNullOrEmpty())
                q = q.Where($@""{{condition}}.Contains(@0)"", keyword);

            return q.GetPagination(pagination).ToList();
        }}

        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name=""id"">主键</param>
        /// <returns></returns>
        public {entityName} GetTheData(string id)
        {{
            return GetEntity(id);
        }}

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name=""newData"">数据</param>
        public void AddData({entityName} newData)
        {{
            Insert(newData);
        }}

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData({entityName} theData)
        {{
            Update(theData);
        }}

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name=""theData"">删除的数据</param>
        public void DeleteData(List<string> ids)
        {{
            Delete(ids);
        }}

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }}
}}";
            string businessPath = _contentRootPath.Replace("Coldairarrow.Web", "Coldairarrow.Business");
            string filePath = Path.Combine(businessPath, areaName, $"{entityName}Business.cs");

            FileHelper.WriteTxt(code, filePath, FileMode.Create);
        }

        /// <summary>
        /// 生成控制器代码
        /// </summary>
        /// <param name="areaName">区域名</param>
        /// <param name="entityName">实体名</param>
        private void BuildController(string areaName, string entityName)
        {
            string varBusiness = $@"_{entityName.ToFirstLowerStr()}Business";
            string code =
$@"using Coldairarrow.Business.{areaName};
using Coldairarrow.Entity.{areaName};
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Coldairarrow.Web
{{
    [Area(""{areaName}"")]
    public class {entityName}Controller : BaseMvcController
    {{
        {entityName}Business {varBusiness} = new {entityName}Business();

        #region 视图功能

        public ActionResult Index()
        {{
            return View();
        }}

        public ActionResult Form(string id)
        {{
            var theData = id.IsNullOrEmpty() ? new {entityName}() : {varBusiness}.GetTheData(id);

            return View(theData);
        }}

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name=""condition"">查询类型</param>
        /// <param name=""keyword"">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(string condition, string keyword, Pagination pagination)
        {{
            var dataList = {varBusiness}.GetDataList(condition, keyword, pagination);

            return Content(pagination.BuildTableResult_DataGrid(dataList).ToJson());
        }}

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name=""theData"">保存的数据</param>
        public ActionResult SaveData({entityName} theData)
        {{
            if(theData.Id.IsNullOrEmpty())
            {{
                theData.Id = Guid.NewGuid().ToSequentialGuid();

                {varBusiness}.AddData(theData);
            }}
            else
            {{
                {varBusiness}.UpdateData(theData);
            }}

            return Success();
        }}

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name=""theData"">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {{
            {varBusiness}.DeleteData(ids.ToList<string>());

            return Success(""删除成功！"");
        }}

        #endregion
    }}
}}";
            string filePath = Path.Combine(_contentRootPath, "Areas", areaName, "Controllers", $"{entityName}Controller.cs");

            FileHelper.WriteTxt(code, filePath, FileMode.Create);
        }

        /// <summary>
        /// 生成视图
        /// </summary>
        /// <param name="tableInfoList">表字段信息</param>
        /// <param name="areaName">区域名</param>
        /// <param name="entityName">实体名</param>
        private void BuildView(List<TableInfo> tableInfoList, string areaName, string entityName)
        {
            //生成Index页面
            StringBuilder searchConditionSelectHtml = new StringBuilder();
            StringBuilder tableColsBuilder = new StringBuilder();
            StringBuilder formRowBuilder = new StringBuilder();

            tableInfoList.Where(x => x.Name != "Id").ForEach((aField, index) =>
                {
                    //搜索的下拉选项
                    Type fieldType = _dbHelper.DbTypeStr_To_CsharpType(aField.Type);
                if (fieldType == typeof(string))
                {
                    string newOption = $@"
                <option value=""{aField.Name}"">{aField.Description}</option>";
                    searchConditionSelectHtml.Append(newOption);
                }

                    //数据表格列
                string end = (index == tableInfoList.Count - 2) ? "" : ",";
                string newCol =$@"
                {{ title: '{aField.Description}', field: '{aField.Name}', width: 200 }}{end}";
                tableColsBuilder.Append(newCol);

                //Form页面中的Html
                string newFormRow = $@"
            <tr>
                <th>{aField.Description}</th>
                <td>
                    <input name=""{aField.Name}"" value=""@obj.{aField.Name}"" class=""easyui-textbox"" data-options=""width:'200px',required:true"">
                </td>
            </tr>";
                formRowBuilder.Append(newFormRow);
            });
            string indexHtml =
$@"@{{
    Layout = ""~/Views/Shared/_Layout_List.cshtml"";
}}

@section toolbar{{
    <a id=""add"" class=""easyui-linkbutton"" data-options=""iconCls:'icon-add'"">添加</a>
    <a id=""edit"" class=""easyui-linkbutton"" data-options=""iconCls:'icon-edit'"">修改</a>
    <a id=""delete"" class=""easyui-linkbutton"" data-options=""iconCls:'icon-remove'"">删除</a>
}}

@section search{{
    <div class=""search_wrapper"">
        <div class=""search_item"">
            <label class=""search_label"">查询类别</label>
            <select name=""condition"" class=""easyui-combobox"" data-options=""width:100"">
                <option value="""">请选择</option>
                {searchConditionSelectHtml.ToString()}
            </select>
            <input name=""keyword"" class=""easyui-textbox"" style=""width:150px"" />
        </div>
        <div class=""search_submit"">
            <a href=""javascript:;"" class=""easyui-linkbutton"" data-options=""iconCls:'icon-search'"" onclick=""searchGrid(this,'#dataTable')"">查询</a>
        </div>
    </div>
}}
<div id=""dataTable"">

</div>

<script>
    var rootUrl = '@Url.Content(""~/"")';
    var formWidth = 500;
    var formHeight = {(tableInfoList.Count - 1) * 35 + 200};

    function initTable() {{
        $('#dataTable').datagrid({{
            url: rootUrl + '{areaName}/{entityName}/GetDataList',
            method: 'POST',
            //queryParams: {{ 'id': id }},
            idField: 'Id',
            fit: true,
            fitColumns: true,
            singleSelect: false,
            selectOnCheck: false,
            checkOnSelect: false,
            //sortName: 'Id',
            //sortOrder: 'asc',
            //rownumbers: true,
            pagination: true,
            pageSize: 30,
            //nowrap: false,
            pageList: [10, 20, 30, 50, 100, 150, 200],
            //showFooter: true,
            columns: [[
                {{ title: 'ck', field: 'ck', checkbox: true }},
                {tableColsBuilder.ToString()}
            ]],
            onBeforeLoad: function (param) {{

            }},
            onBeforeSelect: function () {{
                return false;
            }}
        }});
    }}

    $(function () {{
        initTable();

        //添加数据
        $('#add').click(function () {{
            dialogOpen({{
                id: 'form',
                title: '添加数据',
                width: formWidth,
                height: formHeight,
                url: rootUrl + '{areaName}/{entityName}/Form',
            }});
        }});

        //修改数据
        $('#edit').click(function () {{
            var selected = $(""#dataTable"").datagrid(""getChecked"");
            if (!selected || !selected.length) {{
                dialogError('请选择要修改的记录!');
                return;
            }}
            var id = selected[0].Id;

            dialogOpen({{
                id: 'form',
                title: '修改数据',
                width: formWidth,
                height: formHeight,
                url: rootUrl + '{areaName}/{entityName}/Form?id=' + id,
            }});
        }});

        //删除数据
        $('#delete').click(function () {{
            var checked = $(""#dataTable"").datagrid(""getChecked"");
            if (!checked || !checked.length) {{
                dialogError('请选择要删除的记录!');
                return;
            }}
            var ids = $.map(checked, function (item) {{
                return item['Id'];
            }});

            dialogComfirm('确认删除吗？', function () {{
                $.postJSON(rootUrl + '{areaName}/{entityName}/DeleteData', {{ ids: JSON.stringify(ids) }}, function (resJson) {{
                    if (resJson.Success) {{
                        $('#dataTable').datagrid('clearSelections').datagrid('clearChecked');
                        $('#dataTable').datagrid('reload');
                        dialogMsg('删除成功!');
                    }}
                    else {{
                        dialogError(resJson.Msg);
                    }}
                }});
            }});
        }});
    }});
</script>";
            string indexPath = Path.Combine(_contentRootPath, "Areas", areaName, "Views", entityName, "Index.cshtml");

            FileHelper.WriteTxt(indexHtml, indexPath, FileMode.Create);

            //生成Form页面
            string formHtml = 
$@"@using Coldairarrow.Entity.{areaName};
@using Coldairarrow.Util;

@{{
    Layout = ""~/Views/Shared/_Layout_List.cshtml"";

    var obj = ({entityName})Model;
    var objStr = Html.Raw(obj.ToJson());
}}

<form id=""dataForm"" enctype=""multipart/form-data"" class=""easyui-form"" method=""post"" data-options=""novalidate:true"">
    <table class=""table_base"">
        <colgroup>
            <col style=""width:80px;"" />
        </colgroup>
        <tbody>
            {formRowBuilder.ToString()}
        </tbody>
    </table>
</form>

@section foottoolbar{{
    <a id=""saveForm"" href=""javascript:;"" class=""easyui-linkbutton"" data-options=""iconCls:'icon-save'"">保存</a>
}}

<script>
    var rootUrl = '@Url.Content(""~/"")';
    var theEntity = @objStr;

    $(function () {{
        $('#saveForm').click(function () {{
            if (!$('#dataForm').form('enableValidation').form('validate'))
                return;

            var formValues = $('#dataForm').getValues();
            $.extend(theEntity, formValues);
            $.postJSON(rootUrl + '{areaName}/{entityName}/SaveData', theEntity, function (resJson) {{
                if (resJson.Success) {{
                    parent.dialogMsg('保存成功!');
                    parent.$('#dataTable').datagrid('clearChecked').datagrid('reload');
                    parent.dialogClose('form');
                }}
                else {{
                    dialogError(resJson.Msg);
                }}
            }});
        }});
    }});
</script>
";
            string formPath = Path.Combine(_contentRootPath, "Areas", areaName, "Views", entityName, "Form.cshtml");

            FileHelper.WriteTxt(formHtml, formPath, FileMode.Create);
        }

        /// <summary>
        /// 获取对应的数据库帮助类
        /// </summary>
        /// <param name="linkId">数据库连接Id</param>
        /// <returns></returns>
        private DbHelper GetTheDbHelper(string linkId)
        {
            var theLink = GetTheLink(linkId);
            DbHelper dbHelper = DbHelperFactory.GetDbHelper(theLink.DbType, theLink.ConnectionStr);

            return dbHelper;
        }

        /// <summary>
        /// 获取指定的数据库连接
        /// </summary>
        /// <param name="linkId">连接Id</param>
        /// <returns></returns>
        private Base_DatabaseLink GetTheLink(string linkId)
        {
            Base_DatabaseLink resObj = new Base_DatabaseLink();
            var theModule = GetIQueryable().Where(x => x.Id == linkId).FirstOrDefault();
            resObj = theModule ?? resObj;

            return resObj;
        }
        
        private DbHelper _dbHelper { get; set; }

        private Dictionary<string, DbTableInfo> _dbTableInfoDic { get; set; } = new Dictionary<string, DbTableInfo>();

        private static string _contentRootPath { get; }

        #endregion

        #region 数据模型

        #endregion
    }
}
