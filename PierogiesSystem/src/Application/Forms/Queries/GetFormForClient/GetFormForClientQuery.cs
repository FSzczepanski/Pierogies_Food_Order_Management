using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Forms.Queries.GetFormForClient
{
    public class GetFormForClientQuery : IRequest<FormAmForClient>
    {
        public Guid Id { get; set; }

        public class GetFormQueryHandler : IRequestHandler<GetFormForClientQuery, FormAmForClient>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;
            private readonly IPhotoService _photoService;

            public GetFormQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper, IPhotoService photoService)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
                _photoService = photoService;
            }

            public async Task<FormAmForClient> Handle(GetFormForClientQuery request, CancellationToken cancellationToken)
            {
                FormAmForClient entity = await _applicationDbContext.Forms.AsNoTracking()
                    .ProjectTo<FormAmForClient>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
                
                if (entity == null)
                {
                    throw new NotFoundException(nameof(FormAmForClient), request.Id);
                }
                
                entity.PositionsGrouped = entity.Positions?.GroupBy(p => p.PositionCategory).Select(x => x.ToList()).ToList();
                entity.Positions = null;
                
                return entity;
            }
        }
    }
}