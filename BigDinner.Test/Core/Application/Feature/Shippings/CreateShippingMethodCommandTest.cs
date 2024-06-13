using BigDinner.Application.Common.Abstractions.Repository;
using BigDinner.Application.Features.Shippings.Command;
using BigDinner.Domain.Models.Shippings;

namespace BigDinner.Test.Core.Application.Feature.Shippings;

public class CreateShippingMethodCommandTest
{
    private readonly Mock<IShippingMethodRepository> _shippingMethodRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;

    public CreateShippingMethodCommandTest()
    {
        _shippingMethodRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task CreateShiipingMethod_Should_Return_StatusCode201()
    {
        var query = new CreateShippingMethodCommand() { Name = "Normal Way", Description = "Description test" };
        var handler = new CreateShippingMethodCommandHandler(_mapper, _shippingMethodRepositoryMock.Object, _unitOfWorkMock.Object);

        _shippingMethodRepositoryMock.Setup(x => x.Add(It.IsAny<ShippingMethod>()));
        _unitOfWorkMock.Setup(x => x.CompleteAsync()).Returns(Task.FromResult(1));
        var result =await handler.Handle(query, default);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        _shippingMethodRepositoryMock.Verify(x=>x.Add(It.IsAny<ShippingMethod>()),Times.Exactly(1),"Called more than one");
    }
}
