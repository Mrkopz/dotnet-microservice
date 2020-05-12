using System;
using OrderService.Domain.SeedWork;

namespace OrderService.Domain.Orders
{
    public class OrderId : TypedIdValueBase
    {
        public OrderId(Guid value) : base(value)
        {
        }
    }
}