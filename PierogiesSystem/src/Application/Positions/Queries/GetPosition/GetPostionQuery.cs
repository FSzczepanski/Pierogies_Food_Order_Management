namespace CleanArchitecture.Application.Positions.Queries.GetPosition
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Exceptions;
    using Common.Interfaces;
    using Domain.ValueObjects;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetPositionQuery : IRequest<PositionAm>
    {
        public Guid Id { get; set; }

        public class GetPositionQueryHandler : IRequestHandler<GetPositionQuery, PositionAm>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;
            private readonly IPhotoService _photoService;

            public GetPositionQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper,
                IPhotoService photoService)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
                _photoService = photoService;
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


                if (entity.HasPhoto)
                {
                    Photo photo = await _photoService.GetForParent(entity.Id, cancellationToken);
                    if (photo != null)
                    {
                        entity.Photo = photo;
                    }
                }


                return entity;
            }
        }
    }
}