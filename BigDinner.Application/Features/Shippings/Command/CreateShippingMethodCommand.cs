using BigDinner.Domain.Models.Shippings;

namespace BigDinner.Application.Features.Shippings.Command;

public record CreateShippingMethodCommand : IRequest<Response<string>>
{
    public string Name { get;  set; }

    public string Description { get;  set; }
}

public sealed class CreateShippingMethodCommandHandler : ResponseHandler,
    IRequestHandler<CreateShippingMethodCommand, Response<string>>
{
    private readonly IMapper _mapper;

    private readonly IShippingMethodRepository _shippingMethodRepo;

    private readonly IUnitOfWork _unitOfWork;

    public CreateShippingMethodCommandHandler(
        IMapper mapper,
        IShippingMethodRepository shippingMethodRepo,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _shippingMethodRepo = shippingMethodRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateShippingMethodCommand request, CancellationToken cancellationToken)
    {
        var shippingMethod =  ShippingMethod.Create(request.Name, request.Description);

        _shippingMethodRepo.Add(shippingMethod);

        await _unitOfWork.CompleteAsync();

        return Created("Created");   
    }
}

    