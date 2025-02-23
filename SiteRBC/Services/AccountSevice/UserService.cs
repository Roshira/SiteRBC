using Microsoft.EntityFrameworkCore;
using SiteRBC.Models.Data;
using SiteRBC.Models.SignInAndUpUsers;

namespace SiteRBC.Services.AccountSevice
{
    /**
     * @class UserService
     * @brief Provides implementation of user-related operations such as authentication, registration, and retrieving user profiles.
     * 
     * This service class implements the methods defined in the IUserService interface to handle user authentication, 
     * registration, and profile management, interacting with the database through the SiteRBCContext.
     */
    public class UserService : IUserService
    {
        private readonly SiteRBCContext _context;

        /**
         * @brief Constructor for UserService.
         * @param context The database context for interacting with the application’s data.
         */
        public UserService(SiteRBCContext context)
        {
            _context = context;
        }

        /**
         * @brief Authenticates a user based on their email and password.
         * 
         * This method retrieves the user by email and verifies the provided password against the stored hashed password.
         * 
         * @param email The email address of the user attempting to authenticate.
         * @param password The password entered by the user.
         * @return A task representing the asynchronous operation, with a nullable `Users` object as the result.
         *         Returns the `Users` object if authentication is successful, otherwise returns null.
         */
        public async Task<Users?> AuthenticateUser(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password)) ? user : null;
        }
        
        /**
         * @brief Registers a new user in the system.
         * 
         * This method checks if the user already exists by email. If not, it hashes the password and creates a new user record.
         * 
         * @param model The registration view model containing the user's details.
         * @return A task representing the asynchronous operation, with a boolean indicating success or failure.
         *         Returns true if registration is successful, false if the email is already in use.
         */
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

        /**
         * @brief Retrieves the profile of a user based on their email address.
         * 
         * This method fetches the user's profile information from the database, including their full name and email.
         * 
         * @param email The email address of the user whose profile is to be retrieved.
         * @return A task representing the asynchronous operation, with a nullable `UserProfileViewModel` object as the result.
         *         Returns null if the user does not exist.
         */
        public async Task<UserProfileViewModel?> GetUserProfile(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user == null ? null : new UserProfileViewModel { FullName = user.FullName, Email = user.Email };
        }
    }
}
