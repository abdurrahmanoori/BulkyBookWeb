using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using BulkyBookWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;

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

            //Map or bind secrect and public key from appSetting.json file to StripeSettings class property
            //Note: name of the keys in appSettings.json and class's property must be same in order to map.
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Email authentication service. fake implementaion in order to get ride of exception.
            services.AddSingleton<IEmailSender, EmailSender>();

          //  services.AddDefaultIdentity<ApplicationUser>();





            //services.AddDefaultIdentity<ApplicationUser>().AddDefaultUI().AddEntityFrameworkStores<ApplicationDbContext>();
            //

            //services.AddMvc(Configuration =>  //Apply Authorize attribute globaly.
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //    Configuration.Filters.Add(new AuthorizeFilter(policy));
            //});

            services.Configure<IdentityOptions>(option =>//Override indentity default password ruls.
            {
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 1;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;

            });

            // If you are using Identity framework using Scafholding, chain (add) AddDefaultUI() methode
            //If you don't want to Implement.
            services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders()                
             //.AddDefaultUI()
             .AddEntityFrameworkStores<ApplicationDbContext>();

           // services.AddIdentity<ApplicationUser,ApplicationUser>()



            services.AddControllersWithViews();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

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

            //Assing global api key inside pipeline
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe:SecretKey").Get<string>();

            app.UseAuthentication();
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
