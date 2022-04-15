namespace CleanArchitecture.Application.Orders.Commands
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

    public class UpdateOrderCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        //purchaser
        public string PurchaserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<OrderPosition> Positions { get; set; }
        public DateTime Date { get; set; }
        
        //location
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public bool IsDefault { get; set; }
        
        public Guid FormId { get; set; }
        public decimal DeliveryPrice { get; set; }
        public string Description { get; set; }

        public class Handler : IRequestHandler<UpdateOrderCommand, Guid>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly ILogger<Order> _logger;

            public Handler(IApplicationDbContext applicationDbContext, ILogger<Order> logger)
            {
                _applicationDbContext = applicationDbContext;
                _logger = logger;
            }

            public async Task<Guid> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                Order entity = await _applicationDbContext.Orders.FirstOrDefaultAsync(
                    o => o.Id == request.Id, cancellationToken);
                
                if (entity == null)
                {
                    _logger.LogError("Couldn't find Order with #{id}", request.Id);
                    throw new NotFoundException(nameof(Order), request.Id);
                }
                
                entity.Purchaser = new Purchaser()
                    {Name = request.PurchaserName, Email = request.Email, Phone = request.Phone};
                entity.Positions = request.Positions;
                entity.Date = request.Date;
                entity.Location = new Location()
                {
                    Name = request.LocationName, Description = request.LocationDescription, Street = request.Street,
                    CityName = request.CityName,
                    CountryName = request.CountryName, IsDefault = request.IsDefault, ZipCode = request.ZipCode
                };

                entity.FullPrice = CalculateFullPrice(request.Positions, request.DeliveryPrice, request.IsDefault);

                entity.FormId = request.FormId;
                entity.DeliveryPrice = request.DeliveryPrice;
                entity.Description = request.Description;

                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
            
            private static decimal CalculateFullPrice(List<OrderPosition> orderPositions, decimal deliveryPrice, bool isDefaultLocation)
            {
                decimal fullPrice = 0;
                foreach (var orderPosition in orderPositions)
                {
                    fullPrice += orderPosition.Price * orderPosition.Amount;
                }

                if (deliveryPrice>0)
                {
                    fullPrice += deliveryPrice;
                }

                return fullPrice;
            }
        }
    }
}