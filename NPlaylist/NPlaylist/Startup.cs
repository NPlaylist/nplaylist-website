using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NPlaylist.Infrastructure.Wrappers.DateTimeWrapper;
using NPlaylist.Infrastructure.Wrappers.DirectoryWrapper;
using NPlaylist.Infrastructure.Wrappers.GuidWrapper;
using NPlaylist.Infrastructure.Wrappers.PathWrapper;
using NPlaylist.Authentication;
using NPlaylist.Data;

namespace NPlaylist
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
            });

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            ConfigureDbContexts(services);

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddScoped<IDateTimeWrapper, DateTimeWrapperImpl>();
            services.AddScoped<IDirectoryWrapper, DirectoryWrapperImpl>();
            services.AddScoped<IGuidWrapper, GuidWrapperImpl>();
            services.AddScoped<IPathWrapper, PathWrapperImpl>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ConfigureDbContexts(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();
            services.AddDbContext<AuthenticationDbContext>();
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<AuthenticationDbContext>();
        }
    }
}
