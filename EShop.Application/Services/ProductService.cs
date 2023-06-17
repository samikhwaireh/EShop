using AutoMapper;
using EShop.Application.Mapper;
using EShop.Application.Models;
using EShop.Application.Services.Interfaces;
using EShop.Domain.Entities.ProductCatalog;
using EShop.Domain.Interfaces.Repositories.Base;

namespace EShop.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductResponse>> GetProductByCategory(int categoryId)
    {
        var productList = await unitOfWork.Products.GetAsync(p => p.CategoryId == categoryId);
        var mapped = AppMapper.Mapper.Map<IEnumerable<ProductResponse>>(productList);
        return mapped;
    }

    public async Task<ProductResponse> GetProductById(int productId)
    {
        var product = await unitOfWork.Products.GetByIdAsync(productId);
        var mapped = AppMapper.Mapper.Map<ProductResponse>(product);
        return mapped;
    }

    public async Task<IEnumerable<ProductResponse>> GetProductByName(string productName)
    {
        var product = await unitOfWork.Products.GetAsync(p => p.Name == productName);
        var mapped = AppMapper.Mapper.Map<IEnumerable<ProductResponse>>(product);
        return mapped;
    }

    public async Task<ProductResponse> GetProductBySlug(string slug)
    {
        var productList = await unitOfWork.Products.GetBy(p => p.Slug == slug);
        var mapped = AppMapper.Mapper.Map<ProductResponse>(productList);
        return mapped;
    }

    public async Task<IEnumerable<ProductResponse>> GetProductList()
    {
        var productList = await unitOfWork.Products.GetAllAsync();
        var mapped = AppMapper.Mapper.Map<IEnumerable<ProductResponse>>(productList);
        return mapped;
    }

    public async Task<ProductResponse> Create(ProductResponse product)
    {
        var mappedEntity = AppMapper.Mapper.Map<Product>(product);
        if (mappedEntity == null)
            throw new ApplicationException($"Entity could not be mapped.");

        await unitOfWork.Products.AddAsync(mappedEntity);
        await unitOfWork.SaveChanges();

        return product;
    }

    public async Task Delete(int productId)
    {
        var deletedProduct = await unitOfWork.Products.GetByIdAsync(productId);
        if (deletedProduct == null)
            throw new ApplicationException($"Invalid prodcut Id.");

        unitOfWork.Products.Delete(deletedProduct);
        await unitOfWork.SaveChanges();
    }

    public async Task Update(ProductResponse product)
    {
        var editProduct = await unitOfWork.Products.GetByIdAsync(product.Id);
        if (editProduct == null)
            throw new ApplicationException($"Invalid Product Id.");

        AppMapper.Mapper.Map<ProductResponse, Product>(product, editProduct);

        unitOfWork.Products.Update(editProduct);
        await unitOfWork.SaveChanges();
    }
}
