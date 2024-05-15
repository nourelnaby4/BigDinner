using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Orders.Events;
using BigDinner.Domain.Models.Shippings;

namespace BigDinner.Application.Features.Orders.Events;

public class ShippingOrderCreatedEvent : INotificationHandler<OrderCreatedShippingEvent>
{
    private readonly ICustomerRepository _customerRepository;

    private readonly IShippingRepository _shippingRepository;

    private readonly IUnitOfWork _unitOfWork;

    public ShippingOrderCreatedEvent(
        ICustomerRepository customerRepository,
        IShippingRepository shippingRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _shippingRepository = shippingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(OrderCreatedShippingEvent notification, CancellationToken cancellationToken)
    {
        var eventMessage = notification.EventMessage;

        var shipping = Shipping.Create(eventMessage.orderId, eventMessage.shippingMethodId, eventMessage.address);

        _shippingRepository.Add(shipping);

        await _unitOfWork.CompleteAsync();
    }
}
