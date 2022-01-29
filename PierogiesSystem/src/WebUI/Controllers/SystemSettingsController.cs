namespace CleanArchitecture.WebUI.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.SystemSettings.Commands;
    using Application.SystemSettings.Queries;
    using Domain.Entities;
    using Microsoft.AspNetCore.Mvc;

    public class SystemSettingsController : ApiControllerBase
    {
        [HttpPut]
        public async Task<ActionResult<Guid>> Update([FromBody] SaveSystemSettingsCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }
        
        [HttpGet]
        public async Task<ActionResult<SystemSettings>> Get()
        {
            SystemSettings settings = await Mediator.Send(new GetSystemSettingsQuery());
            return Ok(settings);
        }
    }
}