using System;

namespace OrderService.Domain.SeedWork
{
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            this.OccurredOn = DateTimeOffset.Now;
        }

        public DateTimeOffset OccurredOn { get; }
    }
}