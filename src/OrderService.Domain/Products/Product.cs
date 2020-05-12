using System.Collections.Generic;
using System.Linq;
using OrderService.Domain.SeedWork;
using OrderService.Domain.SharedKernel;

namespace OrderService.Domain.Products
{
    public class Product : Entity, IAggregateRoot
    {
        public ProductId Id { get; private set; }

        public string Name { get; private set; }

        private List<ProductPrice> _prices;

        private Product()
        {

        }

        internal MoneyValue GetPrice(string currency)
        {
            return this._prices.Single(x => x.Value.Currency == currency).Value;
        }
    }
}