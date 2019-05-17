using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NPlaylist.Authentication.Users
{
    public class IdentityUserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityUserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<IdentityUser> FindByIdAsync(string id, CancellationToken ct)
        {
            return _userManager.FindByIdAsync(id);
        }
    }
}
