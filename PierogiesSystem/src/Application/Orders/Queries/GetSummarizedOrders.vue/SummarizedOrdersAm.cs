using System;
using System.Collections.Generic;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Orders.Queries.GetSummarizedOrders.vue
{
    public class SummarizedOrdersAm
    {
        public Guid FormId { get; set; }
        public string FormName { get; set; }
        public ICollection<OrderPosition> Positions { get; set; }
    }
}