
using BigDinner.Domain.Models.Customers;
using System.Text.Json.Serialization;

namespace BigDinner.Application.Features.Customers.Command;

public record EditCustomerRequest(string Phone, Address Address);

public record EditCustomerCommand(Guid CustomerId, string Phone, Address Address) : IRequest<Response<string>>;

public class EditCustomerCommandHandler : ResponseHandler,
    IRequestHandler<EditCustomerCommand, Response<string>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(EditCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer =await _customerRepository.GetByIdAsync(request.CustomerId);

        if (customer is null)
            return NotFound<string>("customer is not found");

        customer.UpdateInformation(request.Phone, request.Address);
       
        _customerRepository.Update(customer);

        await _unitOfWork.CompleteAsync();

        return EditSuccess(string.Empty);
    }
}

