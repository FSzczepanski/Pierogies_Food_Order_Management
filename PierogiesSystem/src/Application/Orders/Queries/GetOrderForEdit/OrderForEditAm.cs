using System;
using System.Collections.Generic;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Orders.Queries.GetOrderForEdit
{
    public class OrderForEditAm : IMapFrom<Order>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<OrderPosition> Positions { get; set; }
        public DateTime Date { get; set; }

        //Location
        public string LocationName { get; set; }
        public string LocationDescription { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public bool IsDefault { get; set; }

        //Payment details
        public PaymentMethodEnum? PaymentMethod { get; set; }
        public bool? IsPaid { get; set; }

        public Guid FormId { get; set; }
        public decimal DeliveryPrice { get; set; }

        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderForEditAm>()
                .ForMember(m => m.Name, o => o.MapFrom(order => order.Purchaser.Name))
                .ForMember(m => m.Email, o => o.MapFrom(order => order.Purchaser.Email))
                .ForMember(m => m.Phone, o => o.MapFrom(order => order.Purchaser.Phone))
                .ForMember(m => m.Positions, o => o.MapFrom(f => f.Positions))
                .ForMember(m => m.PaymentMethod, o => o.MapFrom(order => order.Payment.PaymentMethod))
                .ForMember(m => m.IsPaid, o => o.MapFrom(order => order.Payment.IsPaid))
                .ForMember(m => m.LocationName, o => o.MapFrom(order => order.Location.Name))
                .ForMember(m => m.LocationDescription, o => o.MapFrom(order => order.Location.Description))
                .ForMember(m => m.Street, o => o.MapFrom(order => order.Location.Street))
                .ForMember(m => m.ZipCode, o => o.MapFrom(order => order.Location.ZipCode))
                .ForMember(m => m.CityName, o => o.MapFrom(order => order.Location.CityName))
                .ForMember(m => m.CountryName, o => o.MapFrom(order => order.Location.CountryName))
                .ForMember(m => m.IsDefault, o => o.MapFrom(order => order.Location.IsDefault));
        }
    }
}