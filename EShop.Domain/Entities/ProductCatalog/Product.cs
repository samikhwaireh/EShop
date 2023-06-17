using EShop.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Entities.ProductCatalog;

public class Product : Entity
{
    [Required]
    public string Name { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public string? CoverImage { get; set; }
    public decimal UnitPrice { get; set; }
    public int? UnitsInStock { get; set; }
    public double? Rate { get; set; }
    public string? Size { get; set; }
    public string? Color { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public List<Review>? Reviews { get; set; } = new List<Review>();
    public List<Tag>? Tags { get; set; } = new List<Tag>();

    public static Product Create(int productId, int categoryId, string name, decimal unitPrice = 0, short? unitsInStock = null)
    {
        return new Product
        {
            Id = productId,
            CategoryId = categoryId,
            Name = name,
            UnitPrice = unitPrice,
            UnitsInStock = unitsInStock
        };
    }
}
