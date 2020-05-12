using OrderService.Domain.SeedWork;

namespace OrderService.Domain.Customers
{
    public class CustomerRegisteredEvent : DomainEventBase
    {
        public CustomerId CustomerId { get; }

        public CustomerRegisteredEvent(CustomerId customerId)
        {
            this.CustomerId = customerId;
        }
    }
}