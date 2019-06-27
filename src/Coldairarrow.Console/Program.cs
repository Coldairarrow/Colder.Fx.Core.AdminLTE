using Autofac;
using Autofac.Extras.DynamicProxy;
using Coldairarrow.DataRepository;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Coldairarrow.Console1
{
    class Program
    {
        static Program()
        {
            var builder = new ContainerBuilder();

            var baseType = typeof(IDependency);
            var baseTypeCircle = typeof(ICircleDependency);

            //Coldairarrow相关程序集
            var assemblys = Assembly.GetEntryAssembly().GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Cast<Assembly>()
                .Where(x => x.FullName.Contains("Coldairarrow")).ToList();

            //自动注入IDependency接口,支持AOP,生命周期为InstancePerDependency
            builder.RegisterAssemblyTypes(assemblys.ToArray())
                .Where(x => baseType.IsAssignableFrom(x) && x != baseType)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(Interceptor));

            //自动注入ICircleDependency接口,循环依赖注入,不支持AOP,生命周期为InstancePerLifetimeScope
            builder.RegisterAssemblyTypes(assemblys.ToArray())
                .Where(x => baseTypeCircle.IsAssignableFrom(x) && x != baseTypeCircle)
                .AsImplementedInterfaces()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .InstancePerLifetimeScope();

            //AOP
            builder.RegisterType<Interceptor>();

            AutofacHelper.Container = builder.Build();
        }

        static void Main(string[] args)
        {
            //var db = DbFactory.GetRepository();
            //db.HandleSqlLog = Console.WriteLine;
            List<Task> tasks = new List<Task>();
            LoopHelper.Loop(4, () =>
            {
                tasks.Add(Task.Run(() =>
                {
                    LoopHelper.Loop(100000, () =>
                    {
                        try
                        {
                            string key = Guid.NewGuid().ToString();
                            CacheHelper.Cache.SetCache(key, key, new TimeSpan(0, 10, 0));
                            key = CacheHelper.Cache.GetCache<string>(key);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    });
                }));
            });

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("完成");
            Console.ReadLine();
        }
    }
}
