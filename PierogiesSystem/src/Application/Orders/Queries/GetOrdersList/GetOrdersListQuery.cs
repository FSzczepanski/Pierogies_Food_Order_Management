namespace CleanArchitecture.Application.Orders.Queries.GetOrdersList
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Interfaces;
    using Common.Mappings;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetOrdersListQuery : IRequest<OrderListAm>
    {
        public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, OrderListAm>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;

            public GetOrdersListQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<OrderListAm> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
            {
                var forms = _applicationDbContext.Forms.AsNoTracking();
                
                List<OrderDetailsListAm> items = await _applicationDbContext.Orders.AsNoTracking()
                    .ProjectToListAsync<OrderDetailsListAm>(_mapper.ConfigurationProvider);
                
                foreach (var orderDetailsAm in items)
                {
                    if (orderDetailsAm.FormId != null && orderDetailsAm.FormId!= Guid.Empty)
                    {
                        var form = await forms.FirstOrDefaultAsync(f => f.Id == orderDetailsAm.FormId, cancellationToken);
                        if (form != null) orderDetailsAm.FormName = form.Name;
                    }
                    
                }
                
                return new OrderListAm {Items = items};
            }
        }
        
        
    }
}