using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AspNet_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace AspNet_Core
{
    public class Startup
    {
        private IConfiguration _configur;
        public Startup(IConfiguration configuration)
        {
            _configur = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //add identity
            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
           {
               //configure passowrd complexity 
               option.Password.RequiredLength = 10;
               option.Password.RequiredUniqueChars = 3;
           }
            ).AddEntityFrameworkStores<AppDbContext>();

            ///sql srver servcis
            services.AddDbContextPool<AppDbContext>(options =>
                                      options.UseSqlServer(_configur.GetConnectionString("EmployeeConnectionstring")));
            services.AddMvc(option =>
            {
                var policy = new AuthorizationPolicyBuilder().
                RequireAuthenticatedUser().Build();
                option.Filters.Add(new AuthorizeFilter(policy));
                option.EnableEndpointRouting = false;
            });

            //services delete policy 
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeletRolePolicy",
                policy => policy.RequireClaim("Delete Role"));

                
                options.AddPolicy("EditRolePolicy",
               policy => policy.RequireAssertion(context =>
               context.User.IsInRole("Admin") &&
               context.User.HasClaim(cliam=>cliam.Type=="Edit Role"&& cliam.Value=="true") ||
               context.User.IsInRole("Super Admin")
               ));


            });


            //access denied 
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/administration/AccessDenied");
            });

            services.AddScoped<IEmployeeReository, SqlEmployeeRepository>();
            // services.AddSingleton<IEmployeeReository, MockEmployeeRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            //  app.UseMvcWithDefaultRoute();
            app.UseMvc(Route =>
            Route.MapRoute("defualt", "{controller=home}/{action=index}/{id?}"));


        }
    }
}
