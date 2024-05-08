using BigDinner.Application.Common.Abstractions.Repository;
using BigDinner.Domain.Models.Orders;

namespace BigDinner.Application.Features.Orders.Command;

public record CreateOrderCommand : IRequest<Response<string>>
{
    public Guid OrderNumber { get; set; }

    public DateTime OrderDateOnUtc { get; set; }

    public Guid CustomerId { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public List<OrderItem> Items { get; set;}
}

public record OrderItemDto
{

}

public sealed class CreateOrderCommandHandler : ResponseHandler,
    IRequestHandler<CreateOrderCommand, Response<string>>
{
    private readonly IOrderRepository _orderRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<Response<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}    