namespace EShop.Application.Models;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public string Slug { get; set; }
    public string CoverImage { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? UnitsInStock { get; set; }
    public double Star { get; set; }
    public int? CategoryId { get; set; }
    public CategoryDto Category { get; set; }
}
