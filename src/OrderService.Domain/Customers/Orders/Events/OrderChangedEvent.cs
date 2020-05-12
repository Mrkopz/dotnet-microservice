using OrderService.Domain.SeedWork;

namespace OrderService.Domain.Customers.Orders.Events
{
    public class OrderChangedEvent : DomainEventBase
    {
        public OrderId OrderId { get; }

        public OrderChangedEvent(OrderId orderId)
        {
            this.OrderId = orderId;
        }
    }
}