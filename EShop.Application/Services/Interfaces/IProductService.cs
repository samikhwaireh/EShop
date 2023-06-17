using EShop.Application.Models;
using EShop.Domain.Entities.ProductCatalog;

namespace EShop.Application.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductList();
    Task<ProductDto> GetProductById(int productId);
    Task<ProductDto> GetProductBySlug(string slug);
    Task<IEnumerable<ProductDto>> GetProductByName(string productName);
    Task<IEnumerable<ProductDto>> GetProductByCategory(int categoryId);
    Task<ProductDto> Create(ProductDto productModel);
    Task Update(ProductDto productModel);
    Task Delete(int productId);
}
