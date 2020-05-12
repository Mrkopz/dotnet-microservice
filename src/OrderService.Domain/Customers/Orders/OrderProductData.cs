using OrderService.Domain.Products;

namespace OrderService.Domain.Orders
{
    public class OrderProductData
    {
        public OrderProductData(ProductId productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public ProductId ProductId { get; }

        public int Quantity { get; }
    }
}