using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using System;
using System.Linq.Expressions;
using Coldairarrow.Util;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
using System.Threading;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace Coldairarrow.Console1
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = DbFactory.GetRepository();
            db.HandleSqlLog = Console.WriteLine;
            var list = db.GetIQueryable<Base_User>()
                .Where(x=>EF.Functions.Like(x.RealName,"%aaaa%"))
                .GetPagination(new Pagination())
                .ToList();

            Console.WriteLine("完成");
            Console.ReadLine();
        }
    }
}
