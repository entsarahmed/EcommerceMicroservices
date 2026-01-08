using Catalog.Application.CQRS.Commands;
using Catalog.Application.CQRS.Queries;
using Catalog.Application.ResponseDtos;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers;
public class CatalogController : BaseApiController
{
    private readonly IMediator _mediator;

    public CatalogController(IMediator mediator)
    {
        _mediator=mediator;
    }

    [HttpGet]
    [Route("[action]/{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductResponseDto>> GetProductById(string id)
    {
        var query = new GetProductByIdQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("[action]/{productName}", Name = "GetProductByProductName")]
    [ProducesResponseType(typeof(IList<ProductResponseDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponseDto>> GetProductsByProductName(string productName)
    {
        var query = new GetProductByNameQuery(productName);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    [HttpGet]
    [Route("GetAllProducts")]
    [ProducesResponseType(typeof(IList<ProductResponseDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponseDto>> GetAllProducts([FromQuery] CatalogSpecParams Specs)
    {
        var query = new GetAllProductsQuery(Specs);
        var result = await _mediator.Send(query);
        return Ok(result);
    }


    [HttpGet]
    [Route("GetAllBrands")]
    [ProducesResponseType(typeof(IList<BrandResponseDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<BrandResponseDto>> GetAllBrands()
    {
        var query = new GetAllBrandsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }


    [HttpGet]
    [Route("GetAllTypes")]
    [ProducesResponseType(typeof(IList<TypeResponseDto>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<TypeResponseDto>> GetAllTypes()
    {
        var query = new GetAllTypesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }



    [HttpPost]
    [Route("CreateProduct")]
    [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponseDto>> CreateProduct([FromBody] CreateProductCommand productCommand)
    {
        var result = await _mediator.Send<ProductResponseDto>(productCommand);
        return Ok(result);
    }
    [HttpPut]
    [Route("UpdateProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponseDto>> UpdateProduct([FromBody]UpdateProductCommand productCommand)
    {
        var result = await _mediator.Send<bool>(productCommand);
        return Ok(result);
    }
    [HttpPut]
    [Route("{id}", Name ="DeleteProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductResponseDto>> DeleteProduct(string id)
    {
        var command = new DeleteProductCommand(id);
        var result = await _mediator.Send<bool>(command);
        return Ok(result);
    }
}
