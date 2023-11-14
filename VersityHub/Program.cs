using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VersityHub.VersityHubWebAPI.Customer.Model;
using VersityHub.VersityHubWebAPI.Customer.Seller;
using VersityHub.VersityHubWebAPI.Customer.Services;
using VersityHub.VersityHubWebAPI.Product.Services;
using VersityHub.VersityHubWebAPI.Repository;


namespace VersityHub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddDbContext<ApplicationDBContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerDBContext")));

            builder.Services.AddIdentity<ApplicationCustomer, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddTransient<ISellerService, SellerService>(); 
            builder.Services.AddTransient<IBuyerService, BuyerService>(); 
            builder.Services.AddTransient<IProductService, ProductService>(); 


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}