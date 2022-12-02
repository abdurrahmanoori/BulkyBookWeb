using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBookWeb.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BulkyBookWeb
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
           
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //

            services.AddDefaultIdentity<IdentityUser>().AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //
            services.Configure<IdentityOptions>(option =>//Override indentity default password ruls.
            {
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 1;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
            });

            // If you are using Identity framework using Scafholding, chain (add) AddDefaultUI() methode. 

            //services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
            //    .AddDefaultUI()
            // .AddEntityFrameworkStores<ApplicationDbContext>();

            //Enable mvc project to run Razor Pages.
            services.AddRazorPages();

            services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Identity/Account/Login";  //in your case /Account/Login
        options.LogoutPath = "/Identity/Account/logout";
        options.AccessDeniedPath = "/Visitor/Error/AccessDenied";// In case of access denied.
    });


            //services.AddScoped<IRepository<TModel>, TRepository>();

            //services.AddScoped<ICategoryRepository, ICategoryRepository>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{Area=Customer}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
