using BigDinner.Domain.Models.Customers;

namespace BigDinner.Application.Features.Customers.Query;

public record GetByIdCustomerQuery(Guid customerId): IRequest<Response<GetCustomerQueryResposne>>;

public sealed class GetByIdCustomerQueryHandler : ResponseHandler,
    IRequestHandler<GetByIdCustomerQuery, Response<GetCustomerQueryResposne>>
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;

    public GetByIdCustomerQueryHandler(IMapper mapper, ICustomerRepository customerRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task<Response<GetCustomerQueryResposne>>
        Handle(GetByIdCustomerQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetByIdAsync(request.customerId);

        if(customers is null)
        {
            return NotFound<GetCustomerQueryResposne>("customer is not found");
        }

        var response = _mapper.Map<GetCustomerQueryResposne>(customers);

        return Success(response);
    }
}