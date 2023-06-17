using EShop.Application.Models;

namespace EShop.Application.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponse>> GetCategoryList();
}
