using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NPlaylist.Authentication
{
    public class AuthenticationDbContext : IdentityDbContext
    {
        private readonly IConfiguration _config;

        public AuthenticationDbContext(IConfiguration config, DbContextOptions<AuthenticationDbContext> options)
            : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("AuthenticationDbConnection"), options =>
            {
                options.MigrationsHistoryTable("__UsersMigrationsHistory", "Authentication");
            });
        }
    }
}
