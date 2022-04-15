using CleanArchitecture.Application.Orders.Queries.GetOrder;
using CleanArchitecture.Application.Orders.Queries.GetOrderForEdit;
using CleanArchitecture.Application.Orders.Queries.GetSummarizedOrders.vue;
using CleanArchitecture.WebUI.Filters;

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
        [AuthorizeUser]
        [HttpGet]
        public async Task<ActionResult<OrderListAm>> GetOrders()
        {
            var list = await Mediator.Send(new GetOrdersListQuery());
            return Ok(list);
        }
        
        [AuthorizeUser]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderAm>> Get(Guid id)
        {
            OrderAm model = await Mediator.Send(new GetOrderQuery() { Id = id });
            return Ok(model);
        }
        
        [AuthorizeUser]
        [HttpGet("forEdit/{id:guid}")]
        public async Task<ActionResult<OrderForEditAm>> GetForEdit(Guid id)
        {
            var model = await Mediator.Send(new GetOrderForEditQuery() { Id = id });
            return Ok(model);
        }
        
        [AuthorizeUser]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateOrderCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }
        
        [AuthorizeUser]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> Update([FromBody] UpdateOrderCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }
        
        [AuthorizeUser]
        [HttpGet]
        [Route("summary/{formId:guid}")]
        public async Task<ActionResult<SummarizedOrdersAm>> GetSummarizedOrders(Guid formId)
        {
            SummarizedOrdersAm model = await Mediator.Send(new GetSummarizedOrders { FormId = formId });
            return Ok(model);
        }
    }
}