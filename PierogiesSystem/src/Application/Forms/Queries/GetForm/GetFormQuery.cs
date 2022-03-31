namespace CleanArchitecture.Application.Forms.Queries.GetForm
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

    public class GetFormQuery : IRequest<FormAm>
    {
        public Guid Id { get; set; }

        public class GetFormQueryHandler : IRequestHandler<GetFormQuery, FormAm>
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

            public async Task<FormAm> Handle(GetFormQuery request, CancellationToken cancellationToken)
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