namespace ECommerce_API.Data
{
    public class ProductCategory
    {
        // Composite key for the many-to-many relationship
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}