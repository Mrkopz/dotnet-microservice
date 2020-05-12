using System;
using System.Collections.Generic;
using System.Linq;
using OrderService.Domain.ForeignExchange;
using OrderService.Domain.Products;
using OrderService.Domain.SeedWork;
using OrderService.Domain.SharedKernel;

namespace OrderService.Domain.Orders
{
    public class Order : Entity
    {
        internal OrderId Id;
        private bool _isRemoved;
        private MoneyValue _value;
        private MoneyValue _valueInEUR;
        private List<OrderProduct> _orderProducts;
        private OrderStatus _status;
        private DateTime _orderDate;
        private DateTime? _orderChangeDate;

        private Order()
        {
            this._orderProducts = new List<OrderProduct>();
            this._isRemoved = false;
        }

        private Order(
            List<OrderProductData> orderProductsData,
            List<Product> allProducts,
            string currency, 
            List<ConversionRate> conversionRates
            )
        {
            this._orderDate = DateTime.UtcNow;
            this.Id = new OrderId(Guid.NewGuid());
            this._orderProducts = new List<OrderProduct>();

            foreach (var orderProductData in orderProductsData)
            {
                var product = allProducts.Single(x => x.Id == orderProductData.ProductId);
                var orderProduct = OrderProduct.CreateForProduct(
                    product, 
                    orderProductData.Quantity,
                    currency, 
                    conversionRates);

                _orderProducts.Add(orderProduct);
            }

            this.CalculateOrderValue();
            this._status = OrderStatus.Placed;
        }

        internal static Order CreateNew(List<OrderProductData> orderProductsData,
            List<Product> allProducts,
            string currency,
            List<ConversionRate> conversionRates)
        {
            return new Order(orderProductsData, allProducts, currency, conversionRates);
        }

        internal void Change(
            List<Product> allProducts,
            List<OrderProductData> orderProductsData, 
            List<ConversionRate> conversionRates,
            string currency)
        {
            foreach (var orderProductData in orderProductsData)
            {
                var product = allProducts.Single(x => x.Id == orderProductData.ProductId);
                
                var existingProductOrder = _orderProducts.SingleOrDefault(x => x.ProductId == orderProductData.ProductId);
                if (existingProductOrder != null)
                {
                    var existingOrderProduct = this._orderProducts.Single(x => x.ProductId == existingProductOrder.ProductId);
                    
                    existingOrderProduct.ChangeQuantity(product, orderProductData.Quantity, conversionRates);
                }
                else
                {
                    var orderProduct = OrderProduct.CreateForProduct(product, orderProductData.Quantity, currency, conversionRates);
                    this._orderProducts.Add(orderProduct);
                }
            }

            var orderProductsToCheck = _orderProducts.ToList();
            foreach (var existingProduct in orderProductsToCheck)
            {
                var product = orderProductsData.SingleOrDefault(x => x.ProductId == existingProduct.ProductId);
                if (product == null)
                {
                    this._orderProducts.Remove(existingProduct);
                }
            }

            this.CalculateOrderValue();

            this._orderChangeDate = DateTime.UtcNow;
        }

        internal void Remove()
        {
            this._isRemoved = true;
        }

        internal bool IsOrderedToday()
        {
           return this._orderDate.Date == DateTime.UtcNow.Date;
        }

        private void CalculateOrderValue()
        {
            var value = this._orderProducts.Sum(x => x.Value.Value);
            this._value = new MoneyValue(value, this._orderProducts.First().Value.Currency);

            var valueInEUR = this._orderProducts.Sum(x => x.ValueInEUR.Value);            
            this._valueInEUR = new MoneyValue(valueInEUR, "EUR");
        }
    }
}