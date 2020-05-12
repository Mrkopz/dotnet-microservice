using System;
using MediatR;

namespace OrderService.Domain.SeedWork
{
    public interface IDomainEvent : INotification
    {
        DateTimeOffset OccurredOn { get; }
    }
}