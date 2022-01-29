namespace CleanArchitecture.Application.Orders.Queries.GetOrdersList
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Common.Mappings;
    using Domain.Entities;
    using Domain.ValueObjects;

    public class OrderDetailsListAm : IMapFrom<Order>
    {
        public string PurchaserName { get; set; }
        
        public DateTime Date { get; set; }
        public string LocationString { get; set; }
        
        public bool IsPaid { get; set; }
        public bool NeedInvoice { get; set; }
        
        public Guid FormId { get; set; }
        public string FormName { get; set; }
        
        public decimal FullPrice { get; set; }
        
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDetailsListAm>()
                .ForMember(m => m.PurchaserName, o => o.MapFrom(or => or.Purchaser.Name))
                .ForMember(m => m.LocationString,
                    o => o.MapFrom(or => or.Location.ZipCode + " " + or.Location.CityName + ", " + or.Location.Street))
                .ForMember(m => m.IsPaid, o => o.MapFrom(or => or.Payment.IsPaid))
                .ForMember(m => m.NeedInvoice, o => o.MapFrom(or => or.Payment.NeedInvoice));
        }
    }
}