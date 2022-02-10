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

    public class DeletePositionCommand : IRequest<Guid>
    {
        public Guid Id { get; }
        
        public DeletePositionCommand(Guid id)
        {
            Id = id;
        }


        public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, Guid>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly ILogger<Position> _logger;

            public DeletePositionCommandHandler(IApplicationDbContext applicationDbContext, ILogger<Position> logger)
            {
                _applicationDbContext = applicationDbContext;
                _logger = logger;
            }

            public async Task<Guid> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
            {
                var entity = await
                    _applicationDbContext.Positions.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    _logger.LogError("Couldn't find Position to delete with #{id}", request.Id);
                    throw new NotFoundException(nameof(Position), request.Id);
                }

                _applicationDbContext.Positions.Remove(entity);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}