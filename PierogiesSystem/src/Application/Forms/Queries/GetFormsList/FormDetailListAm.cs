﻿namespace CleanArchitecture.Application.Forms.Queries.GetFormsList
{
    using AutoMapper;
    using Common.Mappings;
    using Domain.Entities;
    using Domain.Enums;
    using Domain.ValueObjects;

    public class FormDetailListAm : IMapFrom<Form>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AvailableDate FormActive { get; set; } 
        public bool IsActive { get; set; } = false;
        public string FormType { get; set; }
        public decimal? DeliveryPrice { get; set; }
        public int PlaceOnList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Form, FormDetailListAm>()
                .ForMember(m => m.FormType, o => o.MapFrom(f => f.FormType.ToString()));
        }
    }
}