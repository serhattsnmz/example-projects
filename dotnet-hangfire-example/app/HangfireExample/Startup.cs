using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Hangfire.SqlServer;
using HangfireExample.Library.Filters;
using HangfireExample.Library.Services;

namespace HangfireExample
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;
        public Startup(IConfiguration _configuration, IWebHostEnvironment _env)
        {
            configuration = _configuration;
            env = _env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<SomeService>();

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseRedisStorage("localhost:6379", new Hangfire.Pro.Redis.RedisStorageOptions
                {
                    Database = 0,
                    Prefix = "{hangfire-1}:"
                })
                //.UseSqlServerStorage("server=(localdb)\\MSSQLLocalDB;database=HangfireTest;integrated security=true;", new SqlServerStorageOptions
                //{
                //    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                //    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                //    QueuePollInterval = TimeSpan.Zero,
                //    UseRecommendedIsolationLevel = true,
                //    UsePageLocksOnDequeue = true,
                //    DisableGlobalLocks = true
                //})
                );

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            // SETTINGS
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddSession(option => { option.IdleTimeout = TimeSpan.FromHours(2); });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHangfireDashboard("/test-hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireFilter() }
            });

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}"
                   );
            });
        }
    }
}
