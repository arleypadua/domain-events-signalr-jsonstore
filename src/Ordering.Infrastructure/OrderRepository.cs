using Ordering.Service.Model;
using Ordering.Service.Repositories;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderCollection _collection;

        public OrderRepository(OrderCollection collection)
        {
            _collection = collection;
        }

        public Task Add(Order order)
        {
            _collection.Add(order);

            return _collection.CommitAsync();
        }
    }
}
