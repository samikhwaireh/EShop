using EShop.Domain.Entities.Order;

namespace EShop.Application.Models;

public class OrderDto
{
    public int UserId { get; set; }
    public OrderAddressDto BillingAddress { get; set; }
    public OrderAddressDto ShippingAddress { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalPrice { get; set; }

    public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
}
