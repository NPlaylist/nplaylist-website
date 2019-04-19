using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NPlaylist.Data;
using NPlaylist.Managers.PathProvider;
using NPlaylist.Managers.TagProvider;
using NPlaylist.Repositories.AudioRepository;
using NPlaylist.Repositories.LocalAudioRepository;
using NPlaylist.Services.AudioService;
using System.IO;
using NPlaylist.Wrappers.DirectoryWrapper;
using NPlaylist.Wrappers.PathWrapper;
using NPlaylist.Wrappers.TagWrapper;

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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddHttpContextAccessor();

            services.AddScoped<IAudioDbRepo, SqlAudioDbRepo>();
            services.AddScoped<IAudioTagsProvider, AudioTagsProvider>();
            services.AddScoped<IAudioLocalRepo, AudioLocalRepo>();
            services.AddScoped<IPathProvider, StandardAudioPathProvider>();
            services.AddScoped<IAudioService, AudioService>();
            services.AddScoped<ITagWrapper, TagLibWrapper>();
            services.AddScoped<IPathWrapper, PathWrapperImpl>();
            services.AddScoped<IDirectoryWrapper, DirectoryWrapperImpl>();

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
    }
}
