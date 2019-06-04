using Coldairarrow.DataRepository;
using System;

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
