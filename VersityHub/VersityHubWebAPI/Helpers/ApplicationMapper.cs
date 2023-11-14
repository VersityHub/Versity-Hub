using AutoMapper;
using VersityHub.VersityHubWebAPI.Product.Model;
using VersityHub.VersityHubWebAPI.Repository;

namespace VersityHub.VersityHubWebAPI.Helpers
{
    public class ApplicationMapper: Profile 
    {
        public ApplicationMapper()
        {
            CreateMap<Products, ProductModel>().ReverseMap();
        }
    }
}
