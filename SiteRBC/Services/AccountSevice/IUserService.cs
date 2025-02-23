using SiteRBC.Models.Data;
using SiteRBC.Models.SignInAndUpUsers;

namespace SiteRBC.Services.AccountSevice
{
    public interface IUserService
    {
        Task<Users?> AuthenticateUser(string email, string password);
        Task<bool> RegisterUser(RegisterViewModel model);
        Task<UserProfileViewModel?> GetUserProfile(string email);
    }
}
