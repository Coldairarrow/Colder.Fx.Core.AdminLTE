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
using Autofac;
using Autofac.Extras.DynamicProxy;
using System.Threading.Tasks;
using Coldairarrow.Util.DotNettySockets;
using Coldairarrow.Util.Sockets;
using System.Threading;
using System.Collections.Concurrent;
using System.Text;

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

            AutofacHelper.Container = builder.Build();
        }

        static async Task RunServerAsync()
        {
            Dictionary<string, DateTime> startTime = new Dictionary<string, DateTime>();
            Dictionary<string, TimeSpan> timeSpan = new Dictionary<string, TimeSpan>();

            var theServer = await SocketBuilderFactory.GetTcpSocketServerBuilder(6001)
                .SetLengthFieldEncoder(2)
                .SetLengthFieldDecoder(ushort.MaxValue, 0, 2, 0, 2)
                .OnConnectionClose((server, connection) =>
                {
                    Console.WriteLine($"连接关闭,连接名[{connection.ConnectionName}]");
                })
                .OnException(ex =>
                {
                    Console.WriteLine($"服务端异常:{ExceptionHelper.GetExceptionAllMsg(ex)}");
                })
                .OnNewConnection((server, connection) =>
                {
                    //connection.ConnectionName = $"名字{connection.ConnectionId}";
                    Console.WriteLine($"新的连接:{connection.ConnectionName}");
                })
                .OnRecieve((server, connection, bytes) =>
                {
                    string key = bytes.ToString(Encoding.UTF8);
                    //Console.WriteLine($"服务端:收到16进制数据{bytes.To0XString().ToUpper()}");
                    timeSpan[key] = DateTime.Now - startTime[key];
                })
                .OnSend((server, connection, bytes) =>
                {
                    Console.WriteLine($"向连接名[{connection.ConnectionName}]发送16进制数据:{bytes.To0XString().ToUpper()}");
                })
                .OnServerStarted(server =>
                {
                    Console.WriteLine($"服务启动");
                }).BuildAsync();

            var theClient = await SocketBuilderFactory.GetTcpSocketClientBuilder("127.0.0.1", 6001)
                .SetLengthFieldEncoder(2)
                .SetLengthFieldDecoder(ushort.MaxValue, 0, 2, 0, 2)
                .OnClientStarted(client =>
                {
                    Console.WriteLine($"客户端启动");
                })
                .OnClientClose(client =>
                {
                    Console.WriteLine($"客户端关闭");
                })
                .OnException(ex =>
                {
                    Console.WriteLine($"异常");
                })
                .OnRecieve((client, bytes) =>
                {
                    Console.WriteLine($"客户端:收到16进制数据{bytes.To0XString().ToUpper()}");
                })
                .OnSend((client, bytes) =>
                {
                    //Console.WriteLine($"客户端:发送16进制数据{bytes.To0XString().ToUpper()}");
                })
                .BuildAsync();
            for (int i = 0; i < 100000; i++)
            {
                string key = Guid.NewGuid().ToString();
                key = key.PadRight(10, 'X');
                startTime[key] = DateTime.Now;
                await theClient.Send(key);
                //Thread.Sleep(100);
            }

            Console.WriteLine($"平均每次耗时:{timeSpan.Average(x => x.Value.Ticks / 10000)}ms");
        }
        static async Task Main(string[] args)
        {
            await RunServerAsync();

            Console.ReadLine();
        }
    }
}