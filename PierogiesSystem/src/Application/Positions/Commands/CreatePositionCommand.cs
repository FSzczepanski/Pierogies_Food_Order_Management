namespace CleanArchitecture.Application.Positions.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using Domain.Entities;
    using Domain.Enums;
    using MediatR;

    public class CreatePositionCommand : IRequest<Guid>
    { 
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public string PortionSize { get; set; }
        public PositionCategoryEnum PositionCategory { get; set; }

        public class Handler : IRequestHandler<CreatePositionCommand, Guid>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public Handler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Guid> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
            {
                var entity = new Position()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Vat = request.Vat,
                    Amount = request.Amount,
                    PortionSize = request.PortionSize,
                    PositionCategory = request.PositionCategory
                };

                await _applicationDbContext.Positions.AddAsync(entity, cancellationToken);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}