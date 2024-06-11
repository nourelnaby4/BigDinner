using BigDinner.Application.Features.Customers.Query;
using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Menus;
using BigDinner.Domain.ValueObjects;


namespace BigDinner.Test.Core.Application.Feature.Customers;

public class GetCustomerByIdTest
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly IMapper _mapper;

    public GetCustomerByIdTest()
    {
        _customerRepositoryMock = new();

        var configuration = new MapperConfiguration(x => x.AddProfile(new GetCustomerProfile()));

        _mapper = new Mapper(configuration);
    }

    public static IEnumerable<object[]> inValidId =>
        new List<object[]>
        {
            new object[] { Guid.NewGuid() }
        };


    [Fact]
    public async void GetCustomerByIdTest_When_GetCustomerById_Return_Customer()
    {
        // Arrange
        var Id = Guid.NewGuid();
        var query = new GetByIdCustomerQuery(Id);
        var handler = new GetByIdCustomerQueryHandler(_mapper, _customerRepositoryMock.Object);
        var customer = Customer.Create("ahmed", new Email("Ahmed@gmail.com"), "01153541474", new Address("cairo", "ahmed arabi", 12259, "Egypt"),Id);
        _customerRepositoryMock.Setup(x => x.GetByIdAsync(Id)).Returns(Task.FromResult(customer));

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Succeeded.Should().BeTrue();
        result.Data.Should().BeOfType<GetCustomerQueryResposne>();
    }


    [Theory]
    [MemberData(nameof(inValidId))]
    public async void GetCustomerByIdTest_When_GetCustomerById_wrong_Return_NotFound(Guid inValidId)
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var query = new GetByIdCustomerQuery(customerId);
        var handler = new GetByIdCustomerQueryHandler(_mapper, _customerRepositoryMock.Object);
        var customer = Customer.Create("ahmed", new Email("Ahmed@gmail.com"), "01153541474", new Address("cairo", "ahmed arabi", 12259, "Egypt"), customerId);
        var customerList = new List<Customer>();
        customerList.Add(customer);
        _customerRepositoryMock.Setup(x => x.GetByIdAsync(inValidId)).Returns(Task.FromResult(customerList.FirstOrDefault(x=>x.Id== inValidId)));

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Succeeded.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
