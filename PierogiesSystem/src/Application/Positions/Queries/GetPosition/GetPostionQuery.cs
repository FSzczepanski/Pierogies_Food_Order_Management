namespace CleanArchitecture.Application.Positions.Queries.GetPosition
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Exceptions;
    using Common.Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetPositionQuery : IRequest<PositionAm>
    {
        public Guid Id { get; set; }
        
        public class GetPositionQueryHandler : IRequestHandler<GetPositionQuery, PositionAm>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetPositionQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<PositionAm> Handle(GetPositionQuery request, CancellationToken cancellationToken)
            {
                PositionAm entity = await _applicationDbContext.Positions.AsNoTracking()
                    .ProjectTo<PositionAm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(PositionAm), request.Id);
                }
                
                return entity;
            }
        }
    }
}