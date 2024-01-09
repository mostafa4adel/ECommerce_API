using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; 

namespace ECommerce_API.Core.Models.Category
{
    public class BaseCategoryDto
    {
        [Required]
        public int Id { get; set; }
        public string CategoryName { get; set; }

    }
}
