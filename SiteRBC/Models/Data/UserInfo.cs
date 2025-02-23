using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SiteRBC.Models.Data
{
    public class UsersInfo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(14)]
        public string Number { get; set; }
    }
}