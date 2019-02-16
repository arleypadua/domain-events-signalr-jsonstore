using System;
using MediatR;

namespace Ordering.Schema.Events
{
    public class OrderPlacedEvent : INotification
    {
        public string OrderId { get; set; }
    }
}