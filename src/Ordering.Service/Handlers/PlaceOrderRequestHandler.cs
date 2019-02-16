using MediatR;
using Ordering.Schema.Commands;
using Ordering.Schema.Events;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ordering.Service.Model;
using Ordering.Service.Repositories;

namespace Ordering.Service.Handlers
{
    public class PlaceOrderRequestHandler : IRequestHandler<PlaceOrderCommand>
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _repository;

        public PlaceOrderRequestHandler(IMediator mediator,
            IOrderRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<Unit> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            // todo make idempotent

            var order = Order.Place(request.CustomerName, request.OrderLines
                .Select(l => Line.New(l.ProductName, l.Quantity))
                .ToArray());

            await _repository.Add(order);

            var eventTasks = order.Events.Select(e => _mediator.Publish(e, cancellationToken));
            await Task.WhenAll(eventTasks);
            
            return Unit.Value;
        }
    }
}