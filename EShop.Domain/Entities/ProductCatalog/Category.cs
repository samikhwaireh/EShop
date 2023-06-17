using EShop.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Entities.ProductCatalog;
public class Category : Entity
{
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? CoverImage { get; set; }

    public static Category Create(int categoryId, string name, string description = null)
    {
        return new Category
        {
            Id = categoryId,
            Name = name,
            Description = description
        };
    }
}

