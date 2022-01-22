namespace CleanArchitecture.Application.Positions.Queries
{
    using System;
    using AutoMapper;
    using Common.Mappings;
    using Domain.Entities;

    public class PositionAm : IMapFrom<Position>
    {
        public Guid Id { get; set; }
        public int IdentityNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public string PortionSize { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Position, PositionAm>();
        }
    }
}