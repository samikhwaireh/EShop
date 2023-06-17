namespace EShop.Application.Models
{
    public class WishlistResponse
    {
        public string UserId { get; set; }
        public List<ProductResponse> Items { get; set; } = new List<ProductResponse>();
    }
}
