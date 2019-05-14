using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NPlaylist.Authentication;
using NPlaylist.Authorization;
using NPlaylist.Business;
using NPlaylist.Business.AudioLogic;
using NPlaylist.Business.LocalRepository;
using NPlaylist.Business.MetaTags;
using NPlaylist.Business.PlaylistLogic;
using NPlaylist.Business.Providers;
using NPlaylist.Business.TagLibWrapper;
using NPlaylist.Infrastructure.System;
using NPlaylist.Persistence;
using NPlaylist.Persistence.AudioEntries;
using NPlaylist.Persistence.AudioMetaEntries;
using NPlaylist.Persistence.PlaylistEntries;
using System.IO;

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
            services.AddScoped<IPlaylistEntriesRepository, PlaylistEntriesRepository>();
            services.AddScoped<IPlaylistService, PlaylistServiceImpl>();
            services.AddScoped<IPathProvider, LocalPathProvider>(a =>
            {
                var hostingEnv = a.GetService<IHostingEnvironment>();
                var rootPath = hostingEnv.WebRootPath + "/Files/";
                return new LocalPathProvider(rootPath, a.GetService<IPathWrapper>(), a.GetService<IGuidWrapper>());
            });

            services.AddScoped<IAudioEntriesRepository, SqlAudioEntriesRepository>();
            services.AddScoped<IAudioLocalRepository, AudioLocalRepositoryImpl>();
            services.AddScoped<IAudioService, AudioServiceImpl>();
            services.AddScoped<IAudioMetaEntriesRepository, SqlAudioMetaEntriesRepository>();

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