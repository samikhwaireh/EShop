using EShop.Domain.Entities.Common;

namespace EShop.Domain.Entities.ProductCatalog;

public class Review : Entity
{
    public string Name { get; set; }
    public string Context { get; set; }
    public double Rate { get; set; }
    public string UserId { get; set; }
}
