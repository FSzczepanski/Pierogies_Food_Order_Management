namespace CleanArchitecture.WebUI.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Positions.Commands;
    using Application.Positions.Queries;
    using Application.Positions.Queries.GetPosition;
    using Application.Positions.Queries.GetPositionsList;
    using Filters;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    [Route("api/v1/core/positions")]
    public class PositionsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PositionListAm>> GetPositions()
        {
            var list = await Mediator.Send(new GetPositionsListQuery());
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreatePositionCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }


        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> Update([FromBody] UpdatePositionCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PositionAm>> Get(Guid id)
        {
            PositionAm model = await Mediator.Send(new GetPositionQuery() { Id = id });
            return Ok(model);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var response = await Mediator.Send(new DeletePositionCommand(id));
            return Ok(response);
        }
    }
}