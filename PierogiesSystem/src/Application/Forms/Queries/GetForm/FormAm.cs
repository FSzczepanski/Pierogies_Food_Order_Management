namespace CleanArchitecture.Application.Forms.Queries.GetForm
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Common.Mappings;
    using Domain.Entities;
    using Domain.Enums;
    using Domain.ValueObjects;

    public class FormAm : IMapFrom<Form>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<FormPosition> Positions { get; set; }
        public List<AvailableDate> AvailableDates { get; set; } 
        public List<PaymentMethodEnum> PaymentMethods { get; set; }
        public List<Location> AvailableLocations { get; set; } 
        public AvailableDate FormActive { get; set; } 
        public bool IsActive { get; set; } = false;
        public FormTypeEnum FormType { get; set; }
        public decimal DeliveryPrice { get; set; } 
        public int PlaceOnList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Form, FormAm>()
                .ForMember(m => m.Positions, o => o.MapFrom(f => f.Positions))
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