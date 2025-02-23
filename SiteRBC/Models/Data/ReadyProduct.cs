using System.ComponentModel.DataAnnotations;

namespace SiteRBC.Models.Data
{
    /**
     * @class ReadyProduct
     * @brief Represents a ready product with various properties.
     * 
     * This class is used to represent a product that is ready for use, with attributes such as 
     * product name, price, dimensions, and construction time.
     */
    public class ReadyProduct
    {
        /**
         * @brief Gets or sets the unique identifier of the product.
         * 
         * This property is the primary key for the product.
         */
        public int Id { get; set; }

        /**
         * @brief Gets or sets the name of the product.
         * 
         * This property is required for each product.
         */
        [Required]
        public string Name { get; set; }

        /**
         * @brief Gets or sets the price of the product.
         * 
         * The price must be greater than zero.
         */
        [Range(0.01, double.MaxValue, ErrorMessage = "Writing price pls")]
        public decimal Price { get; set; }

        /**
         * @brief Gets or sets the height of the product.
         * 
         * The height must be a non-negative value.
         */
        [Range(0, int.MaxValue)]
        public int height { get; set; }

        /**
         * @brief Gets or sets the width of the product.
         * 
         * The width must be a non-negative value.
         */
        [Range(0, int.MaxValue)]
        public int width { get; set; }

        /**
         * @brief Gets or sets the length of the product.
         * 
         * The length must be a non-negative value.
         */
        [Range(0, int.MaxValue)]
        public int length { get; set; }

        /**
         * @brief Gets or sets the time required to build the product.
         * 
         * The build time must be a non-negative value.
         */
        [Range(0, int.MaxValue)]
        public int timeForBuild { get; set; }

        /**
         * @brief Gets or sets the image address of the product.
         * 
         * This property holds the address of the image representing the product.
         */
        public string? AdressImage { get; set; }
    }
}
