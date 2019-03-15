using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using System;
using System.Linq.Expressions;
using Coldairarrow.Util;
using System.Linq;

namespace Coldairarrow.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IRepository db = DbFactory.GetRepository();
            db.HandleSqlLog = log =>
            {
                Console.WriteLine(log);
            };
            Expression<Func<Base_SysLog, Base_User, Base_SysLogDTO>> select = (a, b) => new Base_SysLogDTO
            {
                RealName=b.RealName
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in db.GetIQueryable<Base_SysLog>().AsExpandable()
                    join b in db.GetIQueryable<Base_User>() on a.OpUserName equals b.UserName
                    select @select.Invoke(a, b);

            var list = q.GetPagination(new Pagination()).ToList();

            Console.ReadLine();
        }
    }
}
