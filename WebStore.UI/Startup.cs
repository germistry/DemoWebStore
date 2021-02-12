using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddDbContext<ApplicationDBContext>(options => options
                    .UseSqlServer(Configuration["DefaultConnection"]));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                //TODO 9 Change options to true in Production
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Accounts/Login";
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
                //options.AddPolicy("Manager", policy => policy.RequireClaim("Role", "Manager"));
                options.AddPolicy("Manager", policy => policy.RequireAssertion(context =>
                    context.User.HasClaim("Role", "Admin") ||
                    context.User.HasClaim("Role", "Manager")));
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                //TODO 9 Enable secure cookie in production
                //options.Secure = CookieSecurePolicy.Always;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //TODO 9 Enable HSTS & SuppressXFrameOptionsHeader in Production
            //services.AddHsts(options =>
            //{
            //    options.MaxAge = TimeSpan.FromDays(365);
            //    options.IncludeSubDomains = true;
            //    options.Preload = true;
            //});
            //services.AddAntiforgery(options =>
            //{
            //    //this is set expressly in content headers 
            //    options.SuppressXFrameOptionsHeader = true;
            //});
            services
                .AddMvc(options =>
                {
                    options.CacheProfiles.Add("Monthly", new CacheProfile { Duration = 60 * 60 * 24 * 7 * 4 });
                    //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    options.EnableEndpointRouting = false;
                })
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Admin");
                    options.Conventions.AuthorizePage("/Admin/ConfigureUsers", "Admin");
                })
                .AddFluentValidation(x => x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));
            services.AddSession(options =>
            {
                options.Cookie.Name = "WebStoreCart";
                //Set the session cookie age same as stock expiration date (see Products/GetProduct.cs
                //to ensure stock to return is returned
                options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
            });
         
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            services.AddApplicationServices();
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
            //TODO 9 Enable Security Headers in Production
            //app.Use(async (context, next) =>
            //{
            //    if (!context.Response.Headers.ContainsKey("Header-Name"))
            //    {
            //        //this header just to be be used as the key for this if statement
            //        context.Response.Headers.Add("Header-Name", "Header-Value");
            //        context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            //        context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
            //        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            //        context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
            //        context.Response.Headers.Add("Feature-Policy", "accelerometer 'none'; camera 'none'; geolocation 'none'; gyroscope 'none'; magnetometer 'none'; microphone 'none'; payment 'none'; usb 'none'");
            //        context.Response.Headers.Add("Referrer-Policy", "no-referrer-when-downgrade");
            //        context.Response.Headers.Add("Content-Security-Policy-Report-Only", "default-src 'self'; script-src 'self' https://www.google.com/recaptcha/ https://www.gstatic.com/recaptcha/releases/ 'unsafe-eval'; img-src 'self' data:; report-uri /cspreport");
            //        //context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self' https://www.google.com/recaptcha/ https://www.gstatic.com/recaptcha/releases/ 'unsafe-eval'; img-src 'self' data:;");
            //    }
            //    await next();
            //});
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            //app.UseMvcWithDefaultRoute();

            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            //app.UseAuthorization();

            app.UseMvcWithDefaultRoute();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(name: "default",
                    pattern: "{controller=Admin}/{action=Index}/{id?}");

            });
        }
    }
}
