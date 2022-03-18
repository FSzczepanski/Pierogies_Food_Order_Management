using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Orders.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<OrderAm>
    {
        public Guid Id { get; set; }

        public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderAm>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetOrderQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<OrderAm> Handle(GetOrderQuery request, CancellationToken cancellationToken)
            {
                var entity = await _applicationDbContext.Orders.AsNoTracking()
                    .ProjectTo<OrderAm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
                
                if (entity == null)
                {
                    throw new NotFoundException(nameof(OrderAm), request.Id);
                }

                return entity;
            }
        }
    }
}