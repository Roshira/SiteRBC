using System.ComponentModel.DataAnnotations;

namespace SiteRBC.Models.SignInAndUpUsers
{
    /**
     * @class RegisterViewModel
     * @brief Represents the model used for user registration.
     * 
     * This class contains the necessary properties that a user needs to provide during 
     * the registration process. It includes the user's full name, email, password, 
     * confirmation of the password, and role.
     */
    public class RegisterViewModel
    {
        /**
         * @brief Gets or sets the full name of the user.
         * 
         * This property is required and must have a maximum length of 50 characters.
         */
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        /**
         * @brief Gets or sets the email address of the user.
         * 
         * This property is required and must be a valid email address.
         */
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /**
         * @brief Gets or sets the password of the user.
         * 
         * This property is required, must have a minimum length of 6 characters, 
         * and a maximum length of 50 characters.
         */
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        [MaxLength(50)]
        public string Password { get; set; }

        /**
         * @brief Gets or sets the confirmation of the user's password.
         * 
         * This property is required and must match the value of the Password property.
         */
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        /**
         * @brief Gets or sets the role of the user.
         * 
         * This property defines the role of the user. By default, it is set to "User".
         */
        public string Role { get; set; } = "User";
    }
}
