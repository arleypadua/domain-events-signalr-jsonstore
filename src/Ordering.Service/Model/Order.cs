using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;

namespace Ordering.Service.Model
{
    public class Order
    {
        private Order()
        {
        }

        public static Order Place(string customerName, Line[] orderLines)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(customerName));

            if (orderLines == null || orderLines.Length == 0)
                throw new ArgumentException("Value cannot be null or an empty collection.", nameof(orderLines));
            
            return new Order
            {
                Id = Guid.NewGuid().ToString(),
                CustomerName = customerName,
                _orderLines = new List<Line>(orderLines)
            };
        }

        public string Id { get; private set; }
        public string CustomerName { get; private set; }

        private List<Line> _orderLines;
        public IReadOnlyList<Line> OrderLines
        {
            get => _orderLines.AsReadOnly();
            private set => _orderLines = value.ToList();
        }

        private readonly List<INotification> _events = new List<INotification>();
        internal INotification[] Events => _events.ToArray();

        internal void ClearEvents()
        {
            _events.Clear();
        }
    }
}