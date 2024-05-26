using BigDinner.Domain.Models.Menus;
using BigDinner.Domain.Models.Orders;

namespace BigDinner.Application.Features.Orders.Command;

public record CreateOrderCommand(Guid CustomerId, Address Address, Guid ShippingMethodId, List<CreatedOrderItemDto> Items)
    : IRequest<Response<string>>;

public record CreatedOrderItemDto(Guid MenuId, Guid MenuItemId, int Quantity);

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
        var order = Order.Create(request.CustomerId, request.ShippingMethodId, request.Address);

        foreach (var itemDto in request.Items)
        {
            var menu = await _menuRepository.GetByIdAsync(itemDto.MenuId);

            var menuItem = menu?.Items.FirstOrDefault(x => x.Id == itemDto.MenuItemId);

            if (menuItem is not null)
            {
                order.AddOrderItem(menuItem.Name, itemDto.Quantity, menuItem.Price);
            }
        }

        order.ChangeOrderStatus(OrderStatus.Pending);

        await _orderRepository.AddAsync(order);

        await _unitOfWork.CompleteAsync();

        return Created("Created Successfully");
    }
}