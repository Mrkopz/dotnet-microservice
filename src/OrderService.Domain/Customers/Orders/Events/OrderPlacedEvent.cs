using OrderService.Domain.SeedWork;

namespace OrderService.Domain.Customers.Orders.Events
{
    public class OrderPlacedEvent : DomainEventBase
    {
        public OrderId OrderId { get; }

        public CustomerId CustomerId { get; }

        public OrderPlacedEvent(OrderId orderId, CustomerId customerId)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
        }
    }
}