using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Forms.Queries.GetForm;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Forms.Commands
{
    public class DisableOrEnableFormCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DisableOrEnableFormCommand, Guid>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly ILogger<Form> _logger;

            public Handler(IApplicationDbContext applicationDbContext, ILogger<Form> logger)
            {
                _applicationDbContext = applicationDbContext;
                _logger = logger;
            }

            public async Task<Guid> Handle(DisableOrEnableFormCommand request, CancellationToken cancellationToken)
            {
                var form = await _applicationDbContext.Forms.FirstOrDefaultAsync(form => form.Id == request.Id, cancellationToken);
                
                if (form == null)
                {
                    _logger.LogError("Couldn't find form with id #{id}",request.Id);
                    throw new NotFoundException(nameof(FormAm), request.Id);
                }

                if (form.FormActive != null && form.FormActive.From < DateTime.Now && form.FormActive.To > DateTime.Now)
                {
                    form.FormActive = null;
                    form.IsActive = false;
                }
                else
                {
                    form.FormActive = new AvailableDate {From = DateTime.Now, To = DateTime.Now.AddDays(7)};
                    form.IsActive = true;
                }


                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                
                return form.Id;
            }
        }
    }
}