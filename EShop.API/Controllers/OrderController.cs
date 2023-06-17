using EShop.Application.Models;
using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService orderService;

    public OrderController(IOrderService orderService)
    {
        this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    [Authorize(Roles = "User")]
    [HttpPost("checkout")]
    public async Task<IActionResult> CheckOut(OrderDto orderResponse)
    {
        try
        {
            var checkedOutOrder = await orderService.CheckOut(orderResponse);
            return Ok(checkedOutOrder);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
