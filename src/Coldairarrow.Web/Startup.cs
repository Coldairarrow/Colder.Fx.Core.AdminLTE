using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Coldairarrow.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddControllersAsServices();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton(Configuration);
            services.AddLogging();

            //ʹ��Autofac�滻�Դ�IOC
            var builder = InitAutofac();
            builder.Populate(services);
            var container = builder.Build();

            AutofacHelper.Container = container;

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Request.Body����
            app.Use(next => context =>
            {
                context.Request.EnableRewind();

                return next(context);
            })
            //����
            .UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            )
            .UseDeveloperExceptionPage()
            .UseStaticFiles()
            .UseMvc(routes =>
            {
                //Ĭ��·��
                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                //defaults: new { controller = "Home", action = "Index" }
                );

                //����
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            InitAutoMapper();
        }

        private ContainerBuilder InitAutofac()
        {
            var builder = new ContainerBuilder();

            var baseType = typeof(IDependency);
            var baseTypeCircle = typeof(ICircleDependency);

            //Coldairarrow��س���
            var assemblys = Assembly.GetEntryAssembly().GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Cast<Assembly>()
                .Where(x => x.FullName.Contains("Coldairarrow")).ToList();

            //�Զ�ע��IDependency�ӿ�,֧��AOP,��������ΪInstancePerDependency
            builder.RegisterAssemblyTypes(assemblys.ToArray())
                .Where(x => baseType.IsAssignableFrom(x) && x != baseType)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(Interceptor));

            //�Զ�ע��ICircleDependency�ӿ�,ѭ������ע��,��֧��AOP,��������ΪInstancePerLifetimeScope
            builder.RegisterAssemblyTypes(assemblys.ToArray())
                .Where(x => baseTypeCircle.IsAssignableFrom(x) && x != baseTypeCircle)
                .AsImplementedInterfaces()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .InstancePerLifetimeScope();

            //ע��Controller
            builder.RegisterAssemblyTypes(typeof(Startup).GetTypeInfo().Assembly)
                .Where(t =>typeof(Controller).IsAssignableFrom(t) &&t.Name.EndsWith("Controller", StringComparison.Ordinal))
                .PropertiesAutowired();

            //AOP
            builder.RegisterType<Interceptor>();

            //��������Զ��ͷ�
            builder.RegisterType<DisposableContainer>()
                .As<IDisposableContainer>()
                .InstancePerLifetimeScope();

            return builder;
        }

        /// <summary>
        /// ��ʼ��AutoMapper
        /// </summary>
        private void InitAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Base_User, Base_UserDTO>();
                cfg.CreateMap<Base_SysRole, Base_SysRoleDTO>();
            });
        }
    }
}
