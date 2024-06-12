using BigDinner.Domain.Models.Base;
using BigDinner.Domain.Models.Orders;
using BigDinner.Test.Persistense.Wrapper.interfaces;

namespace BigDinner.Test.Persistense.ExtensionMethods;

public class QueryableExtensionMethodTest
{
    private readonly Mock<IPaginated<Order>> _paginatedServiceMock;

    public QueryableExtensionMethodTest()
    {
        _paginatedServiceMock = new();
    }

    [Theory]
    [InlineData(1,10)]
    public async void ToPaginatedList_Should_Return_NotNull(int pageNumber,int pageSize)
    {
        // Arrange
        var order = Order.Create(Guid.NewGuid(), Guid.NewGuid(), new Address("cairo", "alf maskan", 17765));
        var orderList= new AsyncEnumerable<Order>(new List<Order> { order });
        var paginatedResult= new PaginatedResult<Order>(orderList.ToList());

        // Act
        _paginatedServiceMock.Setup(x => x.ToPaginatedListAsync(orderList, pageNumber, pageSize)).Returns(Task.FromResult(paginatedResult));
        var result=await _paginatedServiceMock.Object.ToPaginatedListAsync(orderList, pageNumber, pageSize);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<PaginatedResult<Order>>();
    }
}
