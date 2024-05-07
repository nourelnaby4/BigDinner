using BigDinner.Domain.Models.Customers;

namespace BigDinner.Application.Features.Customers.Query;

public record GetAllCustomerQuery() : IRequest<Response<IEnumerable<GetCustomerQueryResposne>>>;

public record GetCustomerQueryResposne
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Email Email { get; set; }

    public string Phone { get; set; }

    public Address Address { get; set; }
}


public sealed class GetAllCustomerQueryHandler : ResponseHandler,
    IRequestHandler<GetAllCustomerQuery, Response<IEnumerable<GetCustomerQueryResposne>>>
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomerQueryHandler(IMapper mapper, ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task<Response<IEnumerable<GetCustomerQueryResposne>>>
        Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync();

        var response = _mapper.Map<IEnumerable<GetCustomerQueryResposne>>(customers);

        return Success(response);
    }
}

public sealed class GetAllCustomerProfile : Profile
{
    public GetAllCustomerProfile()
    {
        CreateMap<Customer,GetCustomerQueryResposne>(); 
    }
}