using BigDinner.Domain.Models.Menus;
using BigDinner.Domain.Models.Orders;

namespace BigDinner.Application.Features.Orders.Command;

public record EditOrderCommand(Guid orderId, EditOrderItemDto Item)
: IRequest<Response<string>>;

public record EditOrderItemDto(Guid orderItemId, Guid MenuId, Guid MenuItemId, int Quantity);


public sealed class EditOrderCommandHandler : ResponseHandler,
    IRequestHandler<EditOrderCommand, Response<string>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMenuRepository _menuRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditOrderCommandHandler(
        IOrderRepository orderRepository,
        IMenuRepository menuRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>>
        Handle(EditOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.orderId);

        if (order is null)
            return NotFound<string>();

        if (order.OrderStatus == OrderStatus.Confirmed)
            return BadRequest<string>("order already confirmed, can not updated it");

        var orderItem = order.Items.FirstOrDefault(x => x.Id == request.Item.orderItemId);

        if (order is null)
            return NotFound<string>("order item is not found");

        var menu = await _menuRepository.GetMenuItemByIdAsync(request.Item.MenuId, request.Item.MenuItemId);

        if (menu is null)
            return NotFound<string>("menu is not found");

        var meuItem = menu?.Items.FirstOrDefault();

        orderItem?.UpdateItem(meuItem.Name, request.Item.Quantity, meuItem.Price);

        await _orderRepository.UpdateAsync(order);

        await _unitOfWork.CompleteAsync();

        return EditSuccess("Edit Successfully");
    }
}