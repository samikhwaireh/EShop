using EShop.Application.Models;
using EShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetProductByCategory(int categoryId)
    {
        var products = await productService.GetProductByCategory(categoryId);
        return Ok(products);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductById(int productId)
    {
        var product = await productService.GetProductById(productId);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet("name/{productName}")]
    public async Task<IActionResult> GetProductByName(string productName)
    {
        var products = await productService.GetProductByName(productName);
        return Ok(products);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetProductBySlug(string slug)
    {
        var product = await productService.GetProductBySlug(slug);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetProductList()
    {
        var products = await productService.GetProductList();
        return Ok(products);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(ProductResponse product)
    {
        try
        {
            var createdProduct = await productService.Create(product);
            return CreatedAtAction(nameof(GetProductById), new { productId = createdProduct.Id }, createdProduct);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        try
        {
            await productService.Delete(productId);
            return NoContent();
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> Update(ProductResponse product)
    {
        try
        {
            await productService.Update(product);
            return NoContent();
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
