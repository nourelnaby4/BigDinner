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

        var address = await GetOrderAddress(eventMessage);

        var shipping = Shipping.Create(eventMessage.orderId, eventMessage.shippingMethodId, address, Guid.NewGuid());

        _shippingRepository.Add(shipping);

        await _unitOfWork.CompleteAsync();
    }

    private async Task<Address> GetOrderAddress(OrderCreatedShippingEventMessage eventMessage)
    {
        var address = eventMessage.address;

        if (address is not null)
            return address;

        // incase user dont enter order address save it on customer address
        var customer = await _customerRepository.GetByIdAsync(eventMessage.customerId);

        if (customer is null)
            throw new InvalidDataException("customer is not found");

        if(customer.Address is null)
            throw new InvalidDataException("address is not found");

        address = customer.Address;

        return address;
    }
}
