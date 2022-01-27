namespace CleanArchitecture.Application.Forms.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using Domain.Entities;
    using Domain.Enums;
    using Domain.ValueObjects;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Positions.Queries.GetPositionsList;

    public class CreateFormCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Guid> Positions { get; set; }
        public ICollection<AvailableDate> AvailableDates { get; set; }
        public List<int> PaymentMethods { get; set; }
        public ICollection<Location> AvailableLocations { get; set; }
        public AvailableDate FormActive { get; set; }
        public bool IsActive { get; set; } 
        public FormTypeEnum FormType { get; set; }
        public decimal? DeliveryPrice { get; set; }
        public int PlaceOnList { get; set; }

        public class Handler : IRequestHandler<CreateFormCommand, Guid>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public Handler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Guid> Handle(CreateFormCommand request, CancellationToken cancellationToken)
            {
                var positions = _applicationDbContext.Positions;

                var positionsObjects = new List<FormPosition>();
                
                foreach (var requestPositionId in request.Positions)
                {
                    var positionDb = positions.FirstOrDefault(p => p.Id == requestPositionId);
                    if (positionDb != null)
                    {
                        positionsObjects.Add(new FormPosition
                        {
                            Name = positionDb.Name,
                            Description = positionDb.Description,
                            Amount = positionDb.Amount,
                            Price = positionDb.Price,
                            Vat = positionDb.Vat,
                            PortionSize = positionDb.PortionSize
                        });
                    }
                    
                }
                
                var entity = new Form()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Positions = positionsObjects,
                    AvailableDates = request.AvailableDates,
                    PaymentMethods = request.PaymentMethods,
                    AvailableLocations = request.AvailableLocations,
                    FormActive = request.FormActive,
                    IsActive = request.IsActive,
                    FormType = request.FormType,
                    DeliveryPrice = request.DeliveryPrice,
                    PlaceOnList = request.PlaceOnList
                };
                
                await _applicationDbContext.Forms.AddAsync(entity, cancellationToken);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }

        }
    }
}