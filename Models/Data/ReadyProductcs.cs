using System.ComponentModel.DataAnnotations;

namespace SiteRBC.Models.Data
{
	public class ReadyProductcs
	{
		public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Writing price pls")]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int height { get; set; }
        [Range(0, int.MaxValue)]
        public int width { get; set; }
        [Range(0, int.MaxValue)]
        public int length { get; set; }

        [Range(0, int.MaxValue)]
        public int timeForBuild { get; set; }

        public string AdressImage { get; set; }


	}
}
