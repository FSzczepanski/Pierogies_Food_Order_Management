namespace CleanArchitecture.Application.Forms.Queries.GetForm
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

    public class GetFormListQuery : IRequest<FormAm>
    {
        public Guid Id { get; set; }

        public class GetFormListQueryHandler : IRequestHandler<GetFormListQuery, FormAm>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetFormListQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<FormAm> Handle(GetFormListQuery request, CancellationToken cancellationToken)
            {
                FormAm entity = await _applicationDbContext.Forms.AsNoTracking()
                    .ProjectTo<FormAm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
                
                if (entity == null)
                {
                    throw new NotFoundException(nameof(FormAm), request.Id);
                }
                
                return entity;
            }
        }
    }
}