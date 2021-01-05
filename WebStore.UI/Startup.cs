using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;

using WebStore.Database;

namespace WebStore.UI
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
            services.AddRazorPages();
            services.AddDbContext<ApplicationDBContext>(options => options
                    .UseSqlServer(Configuration["DefaultConnection"]));

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;

            });
            services.AddSession(options =>
            {
                options.Cookie.Name = "WebStoreCart";
                //Set the session cookie age same as stock expiration date (see Products/GetProduct.cs
                //to ensure stock to return is returned
                options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
            });
           
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(name: "default",
                    pattern: "{controller=Admin}/{action=Index}/{id?}");

            });
        }
    }
}
