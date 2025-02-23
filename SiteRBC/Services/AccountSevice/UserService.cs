using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using SiteRBC.Models.SignInAndUpUsers;

namespace SiteRBC.Services.AccountSevice
{
    public class UserService : IUserService
    {
        private readonly SiteRBCContext _context;

        public UserService(SiteRBCContext context)
        {
            _context = context;
        }

        public async Task<Users?> AuthenticateUser(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password)) ? user : null;
        }

        public async Task<bool> RegisterUser(RegisterViewModel model)
        {
            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                return false;

            var user = new Users
            {
                FullName = model.FullName,
                Email = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = model.Role ?? "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserProfileViewModel?> GetUserProfile(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user == null ? null : new UserProfileViewModel { FullName = user.FullName, Email = user.Email };
        }
    }

}
