namespace CleanArchitecture.Application.Forms.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Exceptions;
    using Common.Interfaces;
    using Domain.Entities;
    using Domain.Enums;
    using Domain.ValueObjects;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class UpdateFormCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Position> Positions { get; set; }
        public ICollection<AvailableDate> AvailableDates { get; set; }
        public List<PaymentMethodEnum> PaymentMethods { get; set; }
        public ICollection<Location> AvailableLocations { get; set; }
        public AvailableDate FormActive { get; set; }
        public bool IsActive { get; set; } 
        public FormTypeEnum FormType { get; set; }
        public decimal? DeliveryPrice { get; set; }
        public int PlaceOnList { get; set; }

        public class Handler : IRequestHandler<UpdateFormCommand, Guid>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly ILogger<Form> _logger;

            public Handler(IApplicationDbContext applicationDbContext, ILogger<Form> logger)
            {
                _applicationDbContext = applicationDbContext;
                _logger = logger;
            }

            public async Task<Guid> Handle(UpdateFormCommand request, CancellationToken cancellationToken)
            {
                Form entity = await _applicationDbContext.Forms.FirstOrDefaultAsync(
                    form => form.Id == request.Id, cancellationToken);
                
                if (entity == null)
                {
                    _logger.LogError("Couldn't find Form with #{id}", request.Id);
                    throw new NotFoundException(nameof(Form), request.Id);
                }

                entity.Name = request.Name;
                entity.Description = request.Description;
                entity.Positions = request.Positions;
                entity.AvailableDates = request.AvailableDates;
                entity.PaymentMethods = request.PaymentMethods;
                entity.AvailableLocations = request.AvailableLocations;
                entity.FormActive = request.FormActive;
                entity.IsActive = request.IsActive;
                entity.FormType = request.FormType;
                entity.DeliveryPrice = request.DeliveryPrice;
                entity.PlaceOnList = request.PlaceOnList;
                
                
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}