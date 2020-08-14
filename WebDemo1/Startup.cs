using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebDemo1.DB;

namespace WebDemo1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //依赖注入
            services.AddControllersWithViews();//注册Mvc服务
            services.AddDbContext<ShopContext>(option =>
            {
                option.UseMySql(Configuration["Connection:ConnctionString"]);
            });//注册ShopContext服务
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //配置中间件
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //开启路由中间件
            app.UseRouting();

            //自定义中间件
            //app.Use(async (context,next) =>
            //{
            //    await context.Response.WriteAsync("hkl ");
            //    await next();
            //});

            //开启终结点中间件，主要用来定义路由
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
