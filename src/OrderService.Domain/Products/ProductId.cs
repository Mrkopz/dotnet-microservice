using System;
using OrderService.Domain.SeedWork;

namespace OrderService.Domain.Products
{
    public class ProductId : TypedIdValueBase
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}