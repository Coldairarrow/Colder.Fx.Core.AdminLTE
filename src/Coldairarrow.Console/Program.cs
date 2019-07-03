using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Coldairarrow.Util;

namespace Coldairarrow.Console1
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = DbFactory.GetRepository();

            string userId = "1133345545746780160";
            int count = db.UpdateWhere_Sql(x => x.Id == userId , () => new Base_User { Birthday = DateTime.Now,Sex=1 });

            Console.WriteLine("完成");
            Console.ReadLine();
        }
    }
}