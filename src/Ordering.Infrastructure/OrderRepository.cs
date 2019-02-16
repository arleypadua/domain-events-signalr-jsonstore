using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Ordering.Service.Model;
using Ordering.Service.Repositories;
using System.Threading.Tasks;
using Dapper;
using JsonStore.Sql.Model;
using Microsoft.Extensions.Configuration;

namespace Ordering.Infrastructure
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderCollection _collection;
        private readonly SqlConnection _connection;

        public OrderRepository(OrderCollection collection, IConfiguration configuration)
        {
            _collection = collection;

            _connection = new SqlConnection(configuration.GetConnectionString("OrderingDb"));
            _connection.Open();
        }

        public Task Add(Order order)
        {
            _collection.Add(order);

            return _collection.CommitAsync();
        }

        public async Task<IEnumerable<Order>> ReadAll()
        {
            var documents = await _connection.QueryAsync<SqlDocument>("select * from dbo.[Order]");
            return documents
                .Select(d => _collection.Serializer.Deserialize<Order>(d._Document));
        }
    }
}
