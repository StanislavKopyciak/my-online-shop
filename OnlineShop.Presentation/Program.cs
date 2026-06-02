using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Application.Common.Mappings;
using OnlineShop.Application.Interfaces.Address;
using OnlineShop.Application.Interfaces.Product;
using OnlineShop.Application.Interfaces.User;
using OnlineShop.Application.Services.AddressServices.Commands.AddAddress;
using OnlineShop.Application.Services.AddressServices.Commands.UpdateAddress;
using OnlineShop.Application.Services.AddressServices.Querie.GetAddress;
using OnlineShop.Application.Services.ProductServices.Commands.CreateProduct;
using OnlineShop.Application.Services.ProductServices.Commands.DeleteProduct;
using OnlineShop.Application.Services.ProductServices.Commands.UpdateProduct;
using OnlineShop.Application.Services.ProductServices.Queries.GetProductById;
using OnlineShop.Application.Services.ProductServices.Queries.GetProductsByCategory;
using OnlineShop.Application.Services.ProductServices.Queries.GetProductsBySellerId;
using OnlineShop.Application.Services.ProductServices.Queries.SearchProducts;
using OnlineShop.Application.Services.UserServices.Commands.EditProfile;
using OnlineShop.Application.Services.UserServices.Commands.Login;
using OnlineShop.Application.Services.UserServices.Commands.Register;
using OnlineShop.Application.Services.UserServices.Queries.GetUserByEmail;
using OnlineShop.Application.Services.UserServices.Queries.GetUserById;
using OnlineShop.Infrastructure.Data;
using OnlineShop.Infrastructure.Data.Repository;
using System.Text;
using OnlineShop.Application.Services.UserServices;
using OnlineShop.Infrastructure.Service.Auth;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OnlineShopContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(
    typeof(UserProfile),
    typeof(ProductProfile),
    typeof(AddressProfile)
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();

builder.Services.AddScoped<IValidator<GetUserByIdQuery>, GetUserByIdValidator>();
builder.Services.AddScoped<IValidator<GetUserByEmailQuery>, GetUserByEmailValidator>();
builder.Services.AddScoped<IValidator<RegisterCommand>, RegisterValidator>();
builder.Services.AddScoped<IValidator<LoginCommand>, LoginValidator>();
builder.Services.AddScoped<IValidator<EditProfileCommand>, EditProfileValidator>();

builder.Services.AddScoped<IValidator<GetProductsBySellerIdQuery>, GetProductsBySellerIdValidator>();
builder.Services.AddScoped<IValidator<GetProductsByCategoryQuery>, GetProductsByCategoryValidator>();
builder.Services.AddScoped<IValidator<GetProductByIdQuery>, GetProductByIdValidator>();
builder.Services.AddScoped<GetProductByIdQuery>();
builder.Services.AddScoped<IValidator<UpdateProductCommand>, UpdateProductValidator>();
builder.Services.AddScoped<IValidator<DeleteProductCommand>, DeleteProductValidator>();
builder.Services.AddScoped<IValidator<CreateProductCommand>, CreateProductValidator>();
builder.Services.AddScoped<IValidator<SearchProductsQuery>, SearchProductsValidator>();

builder.Services.AddScoped<IValidator<GetAddressQuery>, GetAddressValidator>();
builder.Services.AddScoped<IValidator<UpdateAddressCommand>, UpdateAddressValidator>();
builder.Services.AddScoped<IValidator<AddAddressCommand>, AddAddressValidator>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<GetUserByIdQuery>();
    cfg.RegisterServicesFromAssemblyContaining<GetUserByEmailQuery>();
    cfg.RegisterServicesFromAssemblyContaining<RegisterCommand>();
    cfg.RegisterServicesFromAssemblyContaining<LoginCommand>();
    cfg.RegisterServicesFromAssemblyContaining<EditProfileCommand>();
    cfg.RegisterServicesFromAssemblyContaining<GetProductsBySellerIdQuery>();
    cfg.RegisterServicesFromAssemblyContaining<GetProductsByCategoryQuery>();
    cfg.RegisterServicesFromAssemblyContaining<GetProductByIdQuery>();
    cfg.RegisterServicesFromAssemblyContaining<UpdateProductCommand>();
    cfg.RegisterServicesFromAssemblyContaining<DeleteProductCommand>();
    cfg.RegisterServicesFromAssemblyContaining<CreateProductCommand>();
    cfg.RegisterServicesFromAssemblyContaining<SearchProductsQuery>();
    cfg.RegisterServicesFromAssemblyContaining<GetAddressQuery>();
    cfg.RegisterServicesFromAssemblyContaining<UpdateAddressCommand>();
    cfg.RegisterServicesFromAssemblyContaining<AddAddressCommand>();
});

var jwtKey = builder.Configuration["SecretKey"] ?? throw new Exception("Нема секретного ключа");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["jwt"];
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/Products/Index"));

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
