using EShop.Application.Mapper;
using EShop.Application.Models;
using EShop.Application.Services.Interfaces;
using EShop.Domain.Interfaces.Repositories.Base;

namespace EShop.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<CategoryResponse>> GetCategoryList()
    {
        var category = await unitOfWork.Categories.GetAllAsync();
        var mapped = AppMapper.Mapper.Map<IEnumerable<CategoryResponse>>(category);
        return mapped;
    }
}
