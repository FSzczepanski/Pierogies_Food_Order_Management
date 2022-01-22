﻿namespace CleanArchitecture.Application.Forms.Commands
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
    using Microsoft.EntityFrameworkCore;
    using Positions.Queries.GetPositionsList;

    public class CreateFormCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Position> Positions { get; set; }
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
                var entity = new Form()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Positions = request.Positions,
                    AvailableDates = request.AvailableDates,
                    PaymentMethods = GetPaymentMethods(request.PaymentMethods),
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
            
            private List<PaymentMethodEnum> GetPaymentMethods(List<int> paymentMethodsInts)
            {
                if (paymentMethodsInts == null)
                {
                    return new List<PaymentMethodEnum>();
                }
                var paymentMethods = new List<PaymentMethodEnum>();
                foreach (var pmInt in paymentMethodsInts)
                {
                    paymentMethods.Add(PaymentMethodEnum.OnPlace);
                }

                return paymentMethods;
            }

        }
    }
}