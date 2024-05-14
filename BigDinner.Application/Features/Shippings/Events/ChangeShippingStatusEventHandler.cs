using BigDinner.Application.Common.Abstractions.Emails;
using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Shippings;
using BigDinner.Domain.Models.Shippings.Events;

namespace BigDinner.Application.Features.Shippings.Events;

public class ChangeShippingStatusEventHandler : INotificationHandler<ChangeShippingStatusEvent>
{
    private readonly IShippingRepository _shippingRepository;

    private readonly ICustomerRepository _customerRepository;

    private readonly IEmailService _emailService;

    public ChangeShippingStatusEventHandler(IShippingRepository shippingRepository,
        ICustomerRepository customerRepository,
        IEmailService emailService)
    {
        _shippingRepository = shippingRepository;
        _customerRepository = customerRepository;
        _emailService = emailService;
    }

    public async Task Handle(ChangeShippingStatusEvent notification, CancellationToken cancellationToken)
    {
        var shipping = await _shippingRepository.GetByIdAsync(notification.eventMessages.shippingId);

        if (shipping is null)
            return;

        var customer = await _customerRepository.GetByIdAsync(shipping.Order.CustomerId);

        if (customer is null)
            return;

        await _emailService.SendEmailAsync(customer.Email.Value, "Order Status", notification.eventMessages.Status.ToString());
    }
}
