using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Orders;

namespace BigDinner.Application.Features.Orders.Events;

public class CreateOrderDomainEvent : INotificationHandler<OrderCreateDomainEvent>
{
    private readonly IEmailService _emailService;

    private readonly ICustomerRepository _customerRepository;

    public CreateOrderDomainEvent(IEmailService emailService, ICustomerRepository customerRepository)
    {
        _emailService = emailService;
        _customerRepository = customerRepository;
    }

    public async Task Handle(OrderCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(notification.order.customerId);

        if (customer is null)
            return;

        await _emailService.SendEmailAsync(customer.Email.Value, "Order Created", "Your Order Is created Successfully");
    }
}
