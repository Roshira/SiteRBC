using System.ComponentModel.DataAnnotations;

namespace SiteRBC.Models.Data
{
    public class CallСustomerBack
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }
    }
}
