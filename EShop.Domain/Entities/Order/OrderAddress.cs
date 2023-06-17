using EShop.Domain.Entities.Common;
using System.Text.Json.Serialization;

namespace EShop.Domain.Entities.Order;

public class OrderAddress : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNo { get; set; }
    public string CompanyName { get; set; }
    public string AddressLine { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    [JsonIgnore]
    public Order BillingOrders { get; set; }
    [JsonIgnore]
    public Order ShippingOrders { get; set; }
}