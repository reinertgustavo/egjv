using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SmartGarcom.Models;
using SmartGarcom.Filters;

namespace SmartGarcom
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
            // Conexão Remota com o Banco
            var connection = @"Server=smartdb.cttfuaqpus95.us-east-1.rds.amazonaws.com;Database=SmartGarcomDB;Trusted_Connection=False;ConnectRetryCount=0;Persist Security Info=False;User ID=usr_smart;Password=Smart!!2018;";
            //var connection = @"Server=localhost;Database=SmartGarcomDB;Trusted_Connection=False;ConnectRetryCount=0;Persist Security Info=False;User ID=SA;Password=Smart@2018;";
            //Conexão Local com o Banco 
        //      var connection = @"Server=(localdb)\mssqllocaldb;Database=SmartGarcomDB;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<Banco>
                (options => options.UseSqlServer(connection));

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(UserInfoFilter));
               // options.Filters.Add(typeof(UserInfoFilter));
            });

            services.AddScoped<AuthAdmin>();
            services.AddScoped<AuthOrderCard>();
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
                    app.UseExceptionHandler("/Home/Error");
                }

                app.UseStaticFiles();
                app.UseCookiePolicy();

                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "areas",
                        template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");                    
                });
            }
        }
    }