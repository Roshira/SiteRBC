using System.ComponentModel.DataAnnotations;

namespace SiteRBC.Models.SignInAndUpUsers
{ /**
     * @class ReadyProduct
     * @brief Represents for do.

     */
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}