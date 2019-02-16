using System.Collections.Generic;
using JsonStore;
using JsonStore.Abstractions;
using Ordering.Service.Model;

namespace Ordering.Infrastructure
{
    public class OrderCollection : Collection<OrderDocument, Order>
    {
        public OrderCollection(IStoreDocuments documentsStore) 
            : base(documentsStore)
        {
        }

        protected override IReadOnlyDictionary<string, object> GetIndexedValuesInternal(OrderDocument document)
        {
            return new Dictionary<string, object>
            {
                [nameof(Order.CustomerName)] = document.Content.CustomerName
            };
        }
    }
}