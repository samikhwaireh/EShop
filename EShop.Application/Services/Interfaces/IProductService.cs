using EShop.Application.Models;
using EShop.Domain.Entities.ProductCatalog;

namespace EShop.Application.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetProductList();
    Task<ProductResponse> GetProductById(int productId);
    Task<ProductResponse> GetProductBySlug(string slug);
    Task<IEnumerable<ProductResponse>> GetProductByName(string productName);
    Task<IEnumerable<ProductResponse>> GetProductByCategory(int categoryId);
    Task<ProductResponse> Create(ProductResponse productModel);
    Task Update(ProductResponse productModel);
    Task Delete(int productId);
}
