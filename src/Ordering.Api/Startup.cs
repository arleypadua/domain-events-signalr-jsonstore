using System.Data.SqlClient;
using System.Reflection;
using JsonStore.Abstractions;
using JsonStore.Sql;
using JsonStore.Sql.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Api.RealTime;
using Ordering.Infrastructure;
using Ordering.Service.Handlers;
using Ordering.Service.Repositories;

namespace Ordering.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMediatR(Assembly.GetExecutingAssembly(),
                typeof(PlaceOrderRequestHandler).Assembly);

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<OrderCollection>();

            services.AddSqlServerJsonStore(Configuration.GetConnectionString("OrderingDb"));
            
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();

            app.UseSignalR(routes =>
            {
                routes.MapHub<OrderingEventsClientHub>("/ordering-events");

                // add your custom routes
            });
        }
    }
}
