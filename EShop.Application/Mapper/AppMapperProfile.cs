using AutoMapper;
using EShop.Domain.Entities.Cart;
using EShop.Domain.Entities.Order;
using EShop.Domain.Entities.ProductCatalog.Wishlists;
using EShop.Domain.Entities.ProductCatalog;
using EShop.Application.Models;

namespace EShop.Application.Mapper;

public class AppMapperProfile : Profile
{
    public AppMapperProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Wishlist, WishlistDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<OrderAddress, OrderAddressDto>().ReverseMap();
        CreateMap<Cart, CartDto>().ReverseMap();
        CreateMap<CartItem, CartItemDto>().ReverseMap();
    }
}
