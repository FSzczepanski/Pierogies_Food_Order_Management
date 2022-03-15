namespace CleanArchitecture.WebUI.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Forms.Commands;
    using Application.Forms.Queries.GetForm;
    using Application.Forms.Queries.GetFormsList;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/core/forms")]
    public class FormsController : ApiControllerBase
    {
        [HttpGet("{justActive:bool?}")]
        public async Task<ActionResult<FormListAm>> GetForms(bool justActive = false)
        {
            FormListAm list = await Mediator.Send(new GetFormsListQuery(){JustActive = justActive});
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateFormCommand command)
        {
            Guid id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> Update([FromBody] UpdateFormCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FormAm>> Get(Guid id)
        {
            FormAm model = await Mediator.Send(new GetFormQuery() { Id = id });
            return Ok(model);
        }
        
    }
}