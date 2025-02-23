using System.ComponentModel.DataAnnotations;

namespace SiteRBC.Models.Data
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
		public string Role { get; set; }
	}
}