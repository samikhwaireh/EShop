﻿using EShop.Application.Models;
using EShop.Domain.Entities.Order;

namespace EShop.Application.Services.Interfaces;

public interface IOrderService
{
    Task<OrderDto> CheckOut(OrderDto orderModel);
}
