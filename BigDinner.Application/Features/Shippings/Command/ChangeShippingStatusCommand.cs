using BigDinner.Application.Common.Abstractions.Repository;
using BigDinner.Domain.Models.Shippings;

namespace BigDinner.Application.Features.Shippings.Command;

public sealed record ChangeShippingStatusCommand(Guid ShippingId, ShippingStatus ShippingStatus) : IRequest<Response<string>>;

public sealed class ChangeShippingStatusCommandHandler : ResponseHandler,
    IRequestHandler<ChangeShippingStatusCommand, Response<string>>
{
    private readonly IShippingRepository _shippingRepo;

    private readonly IUnitOfWork _unitOfWork;

    public ChangeShippingStatusCommandHandler(
        IShippingRepository shippingRepo,
        IUnitOfWork unitOfWork)
    {
        _shippingRepo = shippingRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(ChangeShippingStatusCommand request, CancellationToken cancellationToken)
    {
        var shipping = await _shippingRepo.GetByIdAsync(request.ShippingId);

        if (shipping is null)
            return NotFound<string>("shipping Id is not found");

        shipping.ChangeShippingStatus(request.ShippingStatus);

        await _unitOfWork.CompleteAsync();

        return EditSuccess("change status successfully");
    }
}
