namespace CleanArchitecture.Application.Positions.Queries.GetPositionsList
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Interfaces;
    using Common.Mappings;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetPositionsListQuery : IRequest<PositionListAm>
    {

        public class GetPositionListQueryHandler : IRequestHandler<GetPositionsListQuery, PositionListAm>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetPositionListQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<PositionListAm> Handle(GetPositionsListQuery request, CancellationToken cancellationToken)
            {
                List<PositionAm> items = await _applicationDbContext.Positions.AsNoTracking()
                    .ProjectToListAsync<PositionAm>(_mapper.ConfigurationProvider);

                return new PositionListAm {Items = items};
            }
        }
    }
}