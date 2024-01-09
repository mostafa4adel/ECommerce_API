using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce_API.Data
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }

    }
}
