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
        CreateMap<Product, ProductResponse>();
        CreateMap<Category, CategoryResponse>().ReverseMap();
        CreateMap<Wishlist, WishlistResponse>().ReverseMap();
        CreateMap<Order, OrderResponse>().ReverseMap();
        CreateMap<OrderItem, OrderItemResponse>().ReverseMap();
        CreateMap<OrderAddress, OrderAddressResponse>().ReverseMap();
        CreateMap<Cart, CartResponse>().ReverseMap();
        CreateMap<CartItem, CartItemResponse>().ReverseMap();
    }
}
