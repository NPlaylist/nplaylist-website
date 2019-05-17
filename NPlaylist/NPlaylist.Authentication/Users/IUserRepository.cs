using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NPlaylist.Authentication.Users
{
    public interface IUserRepository
    {
        Task<IdentityUser> FindByIdAsync(string id, CancellationToken ct);
    }
}
