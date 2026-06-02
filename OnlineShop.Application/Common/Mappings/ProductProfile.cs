using AutoMapper;
using OnlineShop.Application.DTOs.Product;
using OnlineShop.Application.Services.ProductServices.Commands.CreateProduct;
using OnlineShop.Application.Services.ProductServices.Commands.UpdateProduct;
using OnlineShop.Application.Services.ProductServices.Queries.GetProductById;
using OnlineShop.Application.Services.ProductServices.Queries.GetProductsByCategory;
using OnlineShop.Application.Services.ProductServices.Queries.GetProductsBySellerId;


namespace OnlineShop.Application.Common.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<CreateProductDTO, CreateProductCommand>();
            CreateMap<UpdateProductDTO, UpdateProductCommand>();

            CreateMap<Core.Entities.Product, GetProductDTO>();
            CreateMap<Core.Entities.Product, GetProductByIdQuery>().ReverseMap();
            CreateMap<Core.Entities.Product, GetProductsByCategoryQuery>().ReverseMap();
            CreateMap<Core.Entities.Product, GetProductsBySellerIdQuery>().ReverseMap();
            CreateMap<GetProductDTO, UpdateProductDTO>();
        }
    }
}
