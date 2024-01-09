using System.ComponentModel.DataAnnotations;


namespace ECommerce_API.Core.Models.Product
{
    public class BaseProductDto
    {
        [Required]
        public int Id { get; set; }

        public string ProductName { get; set; }

    }
}
