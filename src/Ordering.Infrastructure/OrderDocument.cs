using JsonStore;
using Ordering.Service.Model;

namespace Ordering.Infrastructure
{
    public class OrderDocument : Document<Order>
    {
        protected override string GetId() => Content.Id;
    }
}