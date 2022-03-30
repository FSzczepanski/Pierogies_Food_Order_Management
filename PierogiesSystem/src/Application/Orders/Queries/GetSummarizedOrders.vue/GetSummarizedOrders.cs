using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Orders.Queries.GetSummarizedOrders.vue
{
    public class GetSummarizedOrders : IRequest<SummarizedOrdersAm>
    {
        public Guid FormId { get; set; }

        public class GetSummarizedOrdersQueryHandler : IRequestHandler<GetSummarizedOrders,
            SummarizedOrdersAm>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public GetSummarizedOrdersQueryHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<SummarizedOrdersAm> Handle(GetSummarizedOrders request, CancellationToken cancellationToken)
            {
                var orders = _applicationDbContext.Orders.AsNoTracking().Where(order => order.FormId == request.FormId);

                var allPositions = new List<OrderPosition>();
                
                foreach (var order in orders)
                {
                    allPositions.AddRange(order.Positions);
                }
                
                var positions = new List<OrderPosition>();

                allPositions
                    .GroupBy(p => p.PositionId).ToList().ForEach
                        (pos =>
                            positions.Add(new OrderPosition
                            {
                                Name = pos.First().Name,
                                Price = pos.First().Price,
                                Vat = pos.First().Vat,
                                PortionSize = pos.First().PortionSize,
                                PositionId = pos.First().PositionId,
                                Amount = CountAllPositions(pos.ToList())
                            }));


                var formName = _applicationDbContext.Forms.AsNoTracking()
                    .FirstOrDefaultAsync(f => f.Id == request.FormId).Result.Name;

                var summarizedOrders = new SummarizedOrdersAm
                    {Positions = positions, FormId = request.FormId, FormName = formName};

                return summarizedOrders;
            }

            private decimal CountAllPositions(List<OrderPosition> positions)
            {
                decimal amount = 0;
                positions.ForEach(p => amount+=p.Amount);

                return amount;
            }
        }
    }
}