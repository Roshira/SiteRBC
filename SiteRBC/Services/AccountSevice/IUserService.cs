using SiteRBC.Models.Data;
using SiteRBC.Models.SignInAndUpUsers;

namespace SiteRBC.Services.AccountSevice
{
    /**
     * @interface IUserService
     * @brief Defines the contract for user-related operations such as authentication, registration, and retrieving user profiles.
     * 
     * This interface outlines the essential methods that must be implemented by a service class to handle user authentication, 
     * registration, and profile retrieval.
     */
    public interface IUserService
    {
        /**
         * @brief Authenticates a user based on the provided email and password.
         * 
         * This method checks if a user exists with the provided credentials and returns the user details if authentication is successful.
         * 
         * @param email The email address of the user.
         * @param password The password entered by the user.
         * @return A task representing the asynchronous operation, with a nullable `Users` object as the result.
         *         It returns null if the authentication fails.
         */
        Task<Users?> AuthenticateUser(string email, string password);

        /**
         * @brief Registers a new user with the provided details.
         * 
         * This method adds a new user to the system by saving their information to the database.
         * 
         * @param model The registration data model containing the user's information.
         * @return A task representing the asynchronous operation, with a boolean result indicating whether the registration was successful.
         */
        Task<bool> RegisterUser(RegisterViewModel model);

        /**
         * @brief Retrieves the profile of a user based on their email address.
         * 
         * This method fetches the user's profile details such as their full name and email from the database.
         * 
         * @param email The email address of the user whose profile is to be retrieved.
         * @return A task representing the asynchronous operation, with a nullable `UserProfileViewModel` object as the result.
         *         It returns null if no user profile is found.
         */
        Task<UserProfileViewModel?> GetUserProfile(string email);
    }
}
