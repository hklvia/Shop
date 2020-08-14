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
            //����ע��
            services.AddControllersWithViews();//ע��Mvc����
            services.AddDbContext<ShopContext>(option =>
            {
                option.UseMySql(Configuration["Connection:ConnctionString"]);
            });//ע��ShopContext����
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //�����м��
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //����·���м��
            app.UseRouting();

            //�Զ����м��
            //app.Use(async (context,next) =>
            //{
            //    await context.Response.WriteAsync("hkl ");
            //    await next();
            //});

            //�����ս���м������Ҫ��������·��
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
