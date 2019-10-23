using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Coldairarrow.Console1
{
    class Program
    {
        static Program()
        {
            //var httpClientFactory = services.GetService<IHttpClientFactory>();

            //var client = httpClientFactory.CreateClient();
            var builder = new ContainerBuilder();

            var baseType = typeof(IDependency);
            var baseTypeCircle = typeof(ICircleDependency);

            //Coldairarrow相关程序集
            var assemblys = Assembly.GetEntryAssembly().GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Cast<Assembly>()
                .Where(x => x.FullName.Contains("Coldairarrow")).ToList();

            //自动注入IDependency接口,支持AOP
            builder.RegisterAssemblyTypes(assemblys.ToArray())
                .Where(x => baseType.IsAssignableFrom(x) && x != baseType)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(Interceptor));

            //自动注入ICircleDependency接口,循环依赖注入,不支持AOP
            builder.RegisterAssemblyTypes(assemblys.ToArray())
                .Where(x => baseTypeCircle.IsAssignableFrom(x) && x != baseTypeCircle)
                .AsImplementedInterfaces()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .InstancePerLifetimeScope();

            //AOP
            builder.RegisterType<Interceptor>();

            //自带DI
            var services = new ServiceCollection()
                .AddHttpClient();

            builder.Populate(services);

            AutofacHelper.Container = builder.Build();
        }

        static void Main(string[] args)
        {
            var db = DbFactory.GetRepository();
            db.HandleSqlLog = Console.WriteLine;

            Expression<Func<Base_User, Base_Department, Base_UserDTO>> select = (a, b) => new Base_UserDTO
            {
                DepartmentName = b.Name
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in db.GetIQueryable<Base_User>().AsExpandable()
                    join b in db.GetIQueryable<Base_Department>() on a.DepartmentId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);
            var where = LinqHelper.True<Base_UserDTO>();
            where = where.And(x => x.DepartmentName.Contains("xxx"));
            q.Where(where).GetPagination(new Pagination()).ToList();

            //db.GetList<Base_User>();

            Console.WriteLine($"完成");
            Console.ReadLine();
        }
    }
}