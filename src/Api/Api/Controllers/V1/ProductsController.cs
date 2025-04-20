using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.V1;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddProductRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAsync(int id)
    {
        var result = await _mediator.Send(new RemoveProductRequest() { Id = id });
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductRequest request)
    {
        var result = await _mediator.Send(request);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(int id)
    {
        var result = await _mediator.Send(new GetDetailsProductRequest() { Id = id });
        return result is not null ? Ok(result) : NotFound();
    }
    [HttpGet]
    public async Task<IActionResult> GetListAsync()
    {
        var result = await _mediator.Send(new GetListProductRequest());
        return Ok(result);
    }

}
