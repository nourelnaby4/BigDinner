using BigDinner.Application.Common.Abstractions.Repository;
using BigDinner.Application.Features.Menus.Command;
using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Menus;

namespace BigDinner.Application.Features.Customers.Command;

public record CreateCustomerCommand : IRequest<Response<string>>
{
    public string Name { get; set; }

    public Email Email { get; set; }

    public string Phone {  get; set; }

    public Address Address { get; set; }

}

public sealed class CreateCustomerCommandHandler : ResponseHandler,
IRequestHandler<CreateCustomerCommand, Response<string>>
{
    private readonly IMapper _mapper;

    private readonly ICustomerRepository _customerRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(
        IMapper mapper,
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(request.Name, request.Email,request.Phone,request.Address);

        _customerRepository.Add(customer);

        await _unitOfWork.CompleteAsync();

        return Created(string.Empty);
    }
}