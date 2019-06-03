using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using System;
using System.Linq.Expressions;
using Coldairarrow.Util;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Concurrent;

namespace Coldairarrow.Console1
{
    class Program
    {
        /// <summary>
        /// 映射物理表
        /// </summary>
        /// <param name="absTable">抽象表类型</param>
        /// <param name="targetTableName">目标物理表名</param>
        /// <returns></returns>
        public static Type MapTable(Type absTable, string targetTableName)
        {
            var config = TypeBuilderHelper.GetConfig(absTable);

            //实体必须放到Entity层中,不然会出现莫名调试BUG,原因未知
            config.AssemblyName = "Coldairarrow.Entity";
            config.Attributes.RemoveAll(x => x.Attribute == typeof(TableAttribute));
            config.FullName = $"Coldairarrow.Entity.{targetTableName}";

            return TypeBuilderHelper.BuildType(config);
        }

        static void Main(string[] args)
        {
            var db1 = DbFactory.GetRepository();
            db1 = DbFactory.GetRepository();
            //var db2 = DbFactory.GetRepository("MySQL", DatabaseType.MySql);
            //var list1 = db1.GetList<Base_UnitTest>();
            //var list2 = db1.GetList<Base_UnitTest>();

            Base_UnitTest data = new Base_UnitTest
            {
                Id = IdHelper.GetId(),
                UserId = IdHelper.GetId(),
                Age = 10,
                UserName = IdHelper.GetId(),
            };
            var targetType = MapTable(typeof(Base_UnitTest), "Base_UnitTest_0");
            var targetObj = data.ChangeType(targetType);
            db1.Insert(data);
            try
            {
                db1.Insert(targetObj);
            }
            catch (Exception ex)
            {
                string str = string.Empty;
            }
            ConcurrentBag<string> bag = new ConcurrentBag<string>();

            Console.WriteLine("完成");
            Console.ReadLine();
        }
    }
}
