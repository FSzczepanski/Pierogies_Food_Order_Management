namespace CleanArchitecture.WebUI.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Orders.Commands;
    using Application.Orders.Queries.GetOrdersList;
    using Microsoft.AspNetCore.Mvc;

    [Microsoft.AspNetCore.Components.Route("api/v1/core/orders")]
    public class OrderController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<OrderListAm>> GetOrders()
        {
            var list = await Mediator.Send(new GetOrdersListQuery());
            return Ok(list);
        }
        
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateOrderCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> Update([FromBody] UpdateOrderCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }
    }
}