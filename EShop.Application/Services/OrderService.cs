using EShop.Application.Mapper;
using EShop.Application.Models;
using EShop.Application.Services.Interfaces;
using EShop.Domain.Entities.Order;
using EShop.Domain.Interfaces.Repositories.Base;

namespace EShop.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<OrderResponse> CheckOut(OrderResponse orderResponse)
    {
        ValidateOrder(orderResponse);

        var mappedEntity = AppMapper.Mapper.Map<Order>(orderResponse);

        if (mappedEntity == null)
            throw new ApplicationException("Invalid order properity");

        await unitOfWork.Orders.AddAsync(mappedEntity);
        await unitOfWork.SaveChanges();

        return orderResponse;
    }

    private void ValidateOrder(OrderResponse orderResponse)
    {

        if (orderResponse.Items.Count == 0)
        {
            throw new ApplicationException("Order should have at least one item");
        }

        if (orderResponse.Items.Count > 10)
        {
            throw new ApplicationException("Order has maximum 10 items");
        }
    }
}
