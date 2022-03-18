using System;
using System.Collections.Generic;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Orders.Queries.GetOrder
{
    public class OrderAm : IMapFrom<Order>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<OrderPosition> Positions { get; set; }
        public DateTime Created { get; set; }
        public DateTime Date { get; set; }
        public string LocationString { get; set; }
        
        //Payment details
        public PaymentMethodEnum? PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
        public bool? IsPaid { get; set; }
        public bool? NeedInvoice { get; set; }
        
        public Guid FormId { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal FullPrice { get; set; }
        
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderAm>()
                .ForMember(m => m.Name, o => o.MapFrom(order => order.Purchaser.Name))
                .ForMember(m => m.Email, o => o.MapFrom(order => order.Purchaser.Email))
                .ForMember(m => m.Phone, o => o.MapFrom(order => order.Purchaser.Phone))
                .ForMember(m => m.Positions, o => o.MapFrom(f => f.Positions))
                .ForMember(m => m.LocationString,
                    o => o.MapFrom(or => or.Location.ZipCode + " " + or.Location.CityName + ", " + or.Location.Street))
                .ForMember(m => m.PaymentMethod, o => o.MapFrom(order => order.Payment.PaymentMethod))
                .ForMember(m => m.PaymentDate, o => o.MapFrom(order => order.Payment.PaymentDate))
                .ForMember(m => m.IsPaid, o => o.MapFrom(order => order.Payment.IsPaid))
                .ForMember(m => m.NeedInvoice, o => o.MapFrom(order => order.Payment.NeedInvoice));
        }
    }
}