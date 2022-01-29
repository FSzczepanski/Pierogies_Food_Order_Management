namespace CleanArchitecture.Application.Orders.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using Domain.Entities;
    using Domain.Enums;
    using Domain.ValueObjects;
    using MediatR;

    public class CreateOrderCommand : IRequest<Guid>
    {
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
        
        //Payment Details
        public bool? NeedInvoice { get; set; }
        public PaymentMethodEnum? PaymentMethod { get; set; }
        
        public Guid FormId { get; set; }
        public decimal DeliveryPrice { get; set; }
        public string Description { get; set; }

        public class Handler : IRequestHandler<CreateOrderCommand, Guid>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public Handler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var entity = new Order()
                {
                    Purchaser = new Purchaser(){Name = request.PurchaserName, Email = request.Email, Phone = request.Phone},
                    Positions = request.Positions,
                    Date = request.Date,
                    Location = new Location()
                    {
                        Name = request.LocationName, Description = request.LocationDescription, Street = request.Street, CityName = request.CityName,
                        CountryName = request.CountryName, IsDefault = request.IsDefault, ZipCode = request.ZipCode
                    },
                    Payment = new PaymentDetails(){PaymentMethod = request.PaymentMethod,NeedInvoice = request.NeedInvoice, IsPaid = false},
                    FullPrice = CalculateFullPrice(request.Positions, request.DeliveryPrice),
                    FormId = request.FormId,
                    DeliveryPrice = request.DeliveryPrice,
                    Description = request.Description
                };
                
                await _applicationDbContext.Orders.AddAsync(entity, cancellationToken);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }

            private static decimal CalculateFullPrice(List<OrderPosition> orderPositions, decimal deliveryPrice)
            {
                decimal fullPrice = 0;
                foreach (var orderPosition in orderPositions)
                {
                    fullPrice += orderPosition.Price * orderPosition.Amount;
                }

                return fullPrice + deliveryPrice;
            }
        }
    }
}