using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Orders.Queries.GetOrderForEdit
{
    public class GetOrderForEditQuery : IRequest<OrderForEditAm>
    {
        public Guid Id { get; set; }

        public class GetOrderQueryHandler : IRequestHandler<GetOrderForEditQuery, OrderForEditAm>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetOrderQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<OrderForEditAm> Handle(GetOrderForEditQuery request, CancellationToken cancellationToken)
            {
                var entity = await _applicationDbContext.Orders.AsNoTracking()
                    .ProjectTo<OrderForEditAm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
                
                if (entity == null)
                {
                    throw new NotFoundException(nameof(OrderForEditAm), request.Id);
                }

                return entity;
            }
        }
    }
}