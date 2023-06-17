using EShop.Domain.Entities.Common;

namespace EShop.Domain.Entities.Order;
public class Order : Entity
{
    public int UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    //public int BillingAddressId { get; set; }
    //public OrderAddress BillingAddress { get; set; }

    //public int ShippingAddressId { get; set; }
    //public OrderAddress ShippingAddress { get; set; }

    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}
