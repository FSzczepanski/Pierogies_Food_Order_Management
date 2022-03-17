namespace CleanArchitecture.Application.Forms.Queries.GetFormsList
{
    using System;
    using AutoMapper;
    using Common.Mappings;
    using Domain.Entities;
    using Domain.Enums;
    using Domain.ValueObjects;

    public class FormDetailListAm : IMapFrom<Form>
    {
        public Guid Id { get; set; }
        public int IdentityNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AvailableDate FormActive { get; set; } 
        public bool IsActive { get; set; } = false;
        public FormTypeEnum FormType { get; set; }
        public decimal? DeliveryPrice { get; set; }
        public int PlaceOnList { get; set; }
        public decimal? MinimumTotalPrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Form, FormDetailListAm>()
                .ForMember(m => m.IsActive, o => o.MapFrom(
                    form => form.FormActive != null ? form.FormActive.From < DateTime.Now && form.FormActive.To > DateTime.Now : false));
        }
    }
}