using System.Net;
using System.Threading.Tasks;
using BlogExampleStatic.Common.Entities;
using BlogExampleStatic.Web.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;

namespace BlogExampleStatic.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            builder.AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();

            services.AddDbContext<BlogExampleDbContext>(options =>
                    options.UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUserEntity, ApplicationRoleEntity>(o =>
                {
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequiredLength = 4;
                })
                .AddEntityFrameworkStores<BlogExampleDbContext, long>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddRouting(options => options.LowercaseUrls = true);

            //var context = sp.GetService<BlogExampleDbContext>();
            //context.Database.Migrate();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Trace);
            loggerFactory.AddDebug(LogLevel.Trace);
            loggerFactory.AddNLog();

            env.ConfigureNLog("nlog.config");

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseIdentity();

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Post}/{action=Index}/{id?}");
            });
        }
    }
}