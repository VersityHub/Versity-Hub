using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VersityHub.VersityHubWebAPI.Admin.Services;
using VersityHub.VersityHubWebAPI.Config;
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

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(option =>
                {
                    option.SaveToken = true;
                    option.RequireHttpsMetadata = false;
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                    };
                });
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddTransient<ISellerService, SellerService>(); 
            builder.Services.AddTransient<IBuyerService, BuyerService>(); 
            builder.Services.AddTransient<IProductService, ProductService>(); 
            builder.Services.AddTransient<ISetupService, SetupService>(); 
            builder.Services.AddTransient<IJWT_Config, JWT_Config>(); 


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}