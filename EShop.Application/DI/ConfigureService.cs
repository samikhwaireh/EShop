using EShop.Application.Services.Interfaces;
using EShop.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Application.DI;

public static class ConfigureService
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IWishlistService, WishListService>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
