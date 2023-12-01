using System.ComponentModel.DataAnnotations;

namespace VersityHub.VersityHubWebAPI.Repository
{
    public class Products
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
