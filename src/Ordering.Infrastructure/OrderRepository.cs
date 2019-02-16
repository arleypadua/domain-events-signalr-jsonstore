using System;
using System.Threading.Tasks;
using Ordering.Service.Model;
using Ordering.Service.Repositories;

namespace Ordering.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        public Task Add(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
