using Basket.Application.Commands;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Api.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("[action]/{userName}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartItemResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
        {
            var query = new GetBasketByUserNameQuery(userName);
            var basket = await _mediator.Send(query);
            return Ok(basket);
        }

        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartItemResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCartCommand command)
        {
            var basket = await _mediator.Send(command);
            return Ok(basket);
        }
        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> DeleteBasket(string userName)
        {
            var command = new DeleteBasketByUserNameCommand(userName);
            return Ok(await _mediator.Send(command));
        }
    }
}
