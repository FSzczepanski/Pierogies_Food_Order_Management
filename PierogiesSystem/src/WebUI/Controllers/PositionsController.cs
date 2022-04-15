using CleanArchitecture.WebUI.Filters;

namespace CleanArchitecture.WebUI.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Application.Positions.Commands;
    using Application.Positions.Queries;
    using Application.Positions.Queries.GetPosition;
    using Application.Positions.Queries.GetPositionsList;
    using Domain.ValueObjects;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/core/positions")]
    public class PositionsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PositionListAm>> GetPositions()
        {
            var list = await Mediator.Send(new GetPositionsListQuery());
            return Ok(list);
        }

        [AuthorizeUser]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreatePositionCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }


        [AuthorizeUser]
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

        [AuthorizeUser]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var response = await Mediator.Send(new DeletePositionCommand(id));
            return Ok(response);
        }
        
        [AuthorizeUser]
        [HttpPut]
        [Route("photo/{positionId:guid}")]
        public async Task<ActionResult<Guid>> AddPhoto(Guid positionId ,IFormFile file)
        {
            var response = await Mediator.Send(new AddPhotoCommand{Photo = file, ParentId = positionId});
            return Ok(response);
        }
        
        /// <summary>
        /// Get photo for specific id
        /// </summary>
        /// <returns></returns>
        [HttpGet("photo/{positionId:guid}")]
        public async Task<ActionResult> GetPhoto(Guid positionId)
        {
            Photo am = await Mediator.Send(new GetPositionPhotoQuery() { PositionId = positionId });

            if (am?.FileData == null || am.FileData.Length == 0)
            {
                return new NotFoundResult();
            }

            var response = new FileContentResult(am.FileData, am.FileType)
            {
                FileDownloadName = am.PhotoName
            };

            return response;
        }
    }
}