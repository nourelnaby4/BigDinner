using BigDinner.Application.Common.Abstractions.Repository;
using BigDinner.Domain.Models.Menus;
using BigDinner.Domain.Models.Orders;
using BigDinner.Domain.ValueObjects;

namespace BigDinner.Application.Features.Orders.Command;

public record CreateOrderCommand : IRequest<Response<string>>
{
    public Guid OrderNumber { get; set; }

    public DateTime OrderDateOnUtc { get; set; }

    public Guid CustomerId { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public List<OrderItemDto> Items { get; set; }
}

public record OrderItemDto
{
    public Guid MenuId { get;  set; }

    public Guid MenuItemId { get; private set; }

    public int Quantity { get; private set; }
}

public sealed class CreateOrderCommandHandler : ResponseHandler,
    IRequestHandler<CreateOrderCommand, Response<string>>
{
    private readonly IOrderRepository _orderRepository;

    private readonly IMenuRepository _menuRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(
        IOrderRepository orderRepository,
        IMenuRepository menuRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> 
        Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Create(request.CustomerId);

        await AddOrderItemsToOrder(request, order);

        order.ChangeOrderStatus(OrderStatus.Confirmed);

        _orderRepository.Add(order);

        await _unitOfWork.CompleteAsync();

        return Created("Created Successfully");
    }

    private async Task AddOrderItemsToOrder(CreateOrderCommand request, Order order)
    {
        foreach (var itemDto in request.Items)
        {
            var menu = await _menuRepository.GetById(itemDto.MenuId);
            var menuItem = menu?.Items.FirstOrDefault(x => x.Id == itemDto.MenuItemId);

            if (menuItem is not null)
            {
                order.addOrderItem(OrderItem.Create(menuItem.Name, itemDto.Quantity, menuItem.Price));
            }
        }
    }
}