using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Forms.Queries.GetForm;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Forms.Queries.GetFormForClient
{
    public class FormAmForClient : IMapFrom<Form>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<FormPosition>? Positions { get; set; }
        public List<List<FormPosition>> PositionsGrouped { get; set; }
        public List<AvailableDate> AvailableDates { get; set; } 
        public List<PaymentMethodEnum> PaymentMethods { get; set; }
        public List<Location> AvailableLocations { get; set; } 
        public AvailableDate FormActive { get; set; } 
        public bool IsActive { get; set; } = false;
        public FormTypeEnum FormType { get; set; }
        public decimal DeliveryPrice { get; set; } 
        public int PlaceOnList { get; set; }
        public decimal? MinimumTotalPrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Form, FormAmForClient>()
                .ForMember(m => m.Positions, o => o.MapFrom(f => f.Positions))
                .ForMember(m => m.PositionsGrouped, o => o.MapFrom(f => new List<List<FormPosition>>()))
                .ForMember(m => m.AvailableDates, o => o.MapFrom(f => f.AvailableDates))
                .ForMember(m => m.AvailableLocations, o => o.MapFrom(f => f.AvailableLocations))
                .ForMember(m => m.PaymentMethods, o => o.MapFrom(f => HandlePaymentMethods(f.PaymentMethods)));
        }
        
        private static List<PaymentMethodEnum> HandlePaymentMethods(List<int> paymentMethodsInts)
        {
            var pmList = new List<PaymentMethodEnum>();
            if (paymentMethodsInts != null)
            {
                foreach (var paymentMethodsInt in paymentMethodsInts)
                {
                    if (paymentMethodsInt>=1 && paymentMethodsInt<=2)
                    {
                        pmList.Add((PaymentMethodEnum)paymentMethodsInt);
                    }
                }
            }
            
            return pmList;
        }
    }
}