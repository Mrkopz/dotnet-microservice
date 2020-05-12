using System;
using OrderService.Domain.SeedWork;

namespace OrderService.Domain.Customers
{
    public class CustomerId : TypedIdValueBase
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}