using EShop.Domain.Entities.Order;

namespace EShop.Application.Models;

public class OrderResponse
{
    public int UserId { get; set; }
    public OrderAddressResponse BillingAddress { get; set; }
    public OrderAddressResponse ShippingAddress { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalPrice { get; set; }

    public List<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();
}
