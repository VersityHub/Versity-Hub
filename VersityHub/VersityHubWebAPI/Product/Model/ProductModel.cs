using System.ComponentModel.DataAnnotations;

namespace VersityHub.VersityHubWebAPI.Product.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please add Title")]
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }   
}
