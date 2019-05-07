using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NPlaylist.Authentication;
using NPlaylist.Infrastructure.System;
using System.IO;
using AutoMapper;
using NPlaylist.Business;
using NPlaylist.Business.Audio;
using NPlaylist.Business.LocalRepository;
using NPlaylist.Business.MetaTags;
using NPlaylist.Business.Providers;
using NPlaylist.Business.TagLibWrapper;
using NPlaylist.Persistence;
using NPlaylist.Persistence.AudioEntries;
using NPlaylist.Services.AudioService;
using NPlaylist.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace NPlaylist
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
            ConfigureAuthorization(services);

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddScoped<ITagLibWrapper, TagLibWrapper>();
            services.AddScoped<ITagsProvider, TagLibTagsProvider>();
            services.AddScoped<IDateTimeWrapper, DateTimeWrapper>();
            services.AddScoped<IDirectoryWrapper, DirectoryWrapper>();
            services.AddScoped<IGuidWrapper, GuidWrapper>();
            services.AddScoped<IPathWrapper, PathWrapper>();
            services.AddScoped<IFileStreamFactory, FileStreamImpl>();
            services.AddScoped<IFileWrapper, FileWrapper>();
            services.AddScoped<IPathProvider, LocalPathProvider>(a =>
            {
                var hostingEnv = a.GetService<IHostingEnvironment>();
                var rootPath = hostingEnv.WebRootPath + "/Files/";
                return new LocalPathProvider(rootPath, a.GetService<IPathWrapper>(), a.GetService<IGuidWrapper>());
            });

            services.AddScoped<IAudioEntriesRepository, SqlAudioEntriesRepository>();
            services.AddScoped<IAudioLocalRepository, AudioLocalRepositoryImpl>();
            services.AddScoped<Services.AudioService.IAudioService, Services.AudioService.AudioServiceImpl>();
            services.AddScoped<Business.Audio.IAudioService,Business.Audio.AudioServiceImpl >();

            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void ConfigureDbContexts(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ApplicationDbConnection"), options =>
                {
                    options.MigrationsHistoryTable("__UsersMigrationsHistory", "Application");
                });
            });

            services.AddDbContext<AuthenticationDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("AuthenticationDbConnection"), options =>
                {
                    options.MigrationsHistoryTable("__UsersMigrationsHistory", "Authentication");
                });
            });
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<AuthenticationDbContext>();
        }

        private void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, AudioAuthorizationCrudHandler>();
        }
    }
}