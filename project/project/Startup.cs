using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using project.Data;
using project.Models;
using project.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace project
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
            services.AddMvc(services => services.EnableEndpointRouting = false);
            services.AddScoped<IRepository<Car>, dbCarRepository>();
            services.AddScoped<IRepository<CallBack>, dbCallBackRepository>();
            services.AddScoped<IRepository<Contact>, dbContactRepository>();
            services.AddScoped<IRepository<Lastnewsupdate>, dbLastnewsupdateRepository>();
            services.AddScoped<IRepository<Rent>, dbRentRepository>();
            services.AddScoped<IRepository<Sale>, dbSaleRepository>();
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Admin/Account/Login";
                options.AccessDeniedPath = $"/Admin/Account/Login";
            });
            services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString("sqlcon"));
            });
            services.Configure<IdentityOptions>(passwordConfig =>
            {
                passwordConfig.Password.RequireDigit = false;
                passwordConfig.Password.RequireLowercase = false;
                passwordConfig.Password.RequireUppercase = false;
                passwordConfig.Password.RequireNonAlphanumeric = false;
                passwordConfig.Password.RequiredUniqueChars = 0;
                passwordConfig.Password.RequiredLength = 3;
            });
            services.AddControllersWithViews();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseMvc();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(app =>
             {

                 app.MapControllerRoute(
                        name: "areas",
                         pattern: "{area:exists}/{controller=Car}/{action=Index}/{id?}"
                         );

                 app.MapControllerRoute(
                         name: "default",
                         pattern: "{controller=Home}/{action=Index}/{id?}"
                         );

             });
        }
    }
}
