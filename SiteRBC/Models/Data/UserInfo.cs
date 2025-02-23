using System;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
