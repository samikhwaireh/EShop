namespace EShop.Application.Models
{
    public class WishlistDto
    {
        public string UserId { get; set; }
        public List<ProductDto> Items { get; set; } = new List<ProductDto>();
    }
}
