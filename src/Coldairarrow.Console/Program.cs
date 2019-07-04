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
using System.Data;

namespace Coldairarrow.Console1
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = DbFactory.GetRepository();
            db.HandleSqlLog = Console.WriteLine;

            Console.WriteLine("完成");
            Console.ReadLine();
        }
    }
}