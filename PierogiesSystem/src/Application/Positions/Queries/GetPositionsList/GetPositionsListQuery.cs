namespace CleanArchitecture.Application.Positions.Queries.GetPositionsList
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Interfaces;
    using Common.Mappings;
    using Domain.ValueObjects;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetPositionsListQuery : IRequest<PositionListAm>
    {
        public class GetPositionListQueryHandler : IRequestHandler<GetPositionsListQuery, PositionListAm>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;
            private readonly IPhotoService _photoService;

            public GetPositionListQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper,
                IPhotoService photoService)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
                _photoService = photoService;
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