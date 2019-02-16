using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Ordering.Service.Repositories;

namespace Ordering.Api.RealTime
{
    public class OrderingEventsClientHub : Hub
    {
        private readonly IOrderRepository _repository;

        public OrderingEventsClientHub(IOrderRepository repository)
        {
            _repository = repository;
        }

        public override async Task OnConnectedAsync()
        {
            var orders = await _repository.ReadAll();
            var sendTasks = orders.Select(o => Clients.Caller.SendAsync("orderEventsInitialized", o));

            await Task.WhenAll(sendTasks);
        }

        // This is empty and could potentially have methods to also receive the place order request.
    }
}