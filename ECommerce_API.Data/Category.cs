using System.ComponentModel.DataAnnotations.Schema;


namespace ECommerce_API.Data
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
    }
}
