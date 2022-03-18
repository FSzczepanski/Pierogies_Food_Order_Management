using CleanArchitecture.Application.Orders.Queries.GetOrder;

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
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderAm>> Get(Guid id)
        {
            OrderAm model = await Mediator.Send(new GetOrderQuery() { Id = id });
            return Ok(model);
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