namespace CleanArchitecture.WebUI.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Forms.Commands;
    using Application.Forms.Queries.GetFormsList;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/core/forms")]
    public class FormsController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<FormListAm>> GetForms()
        {
            FormListAm list = await Mediator.Send(new GetFormsListQuery());
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateFormCommand command)
        {
            Guid id = await Mediator.Send(command);
            return Ok(command);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> Update([FromBody] UpdateFormCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }
        
    }
}