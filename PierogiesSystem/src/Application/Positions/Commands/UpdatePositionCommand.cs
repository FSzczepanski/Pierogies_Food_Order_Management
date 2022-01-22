namespace CleanArchitecture.Application.Positions.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Exceptions;
    using Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    public class UpdatePositionCommand: IRequest<Guid>
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal Amount { get; set; }
        public string PortionSize { get; set; }

        public class Handler : IRequestHandler<UpdatePositionCommand, Guid>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly ILogger<Position> _logger;

            public Handler(IApplicationDbContext applicationDbContext, ILogger<Position> logger)
            {
                _applicationDbContext = applicationDbContext;
                _logger = logger;
            }

            public async Task<Guid> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
            {

                Position entity = await _applicationDbContext.Positions.FirstOrDefaultAsync(
                    pos => pos.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    _logger.LogError("Couldn't find Position with #{id}", request.Id);
                    throw new NotFoundException(nameof(Position), request.Id);
                }

                entity.Name = request.Name;
                entity.Description = request.Description;
                entity.Price = request.Price;
                entity.Vat = request.Vat;
                entity.Amount = request.Amount;
                entity.PortionSize = request.PortionSize;

                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}
