using AutoMapper;
using BigDinner.Application.Common.Abstractions.Repository;
using BigDinner.Application.Common.Models;
using BigDinner.Application.Features.Menus.Query;
using BigDinner.Domain.Models.Menus;

namespace BigDinner.Test.Core.Application.Feature.Menue;

public class GetMenuQueryHandlerTest
{
    private readonly Mock<IMenuRepository> _menuRepository;
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly GetAllMenuQueryProfile _getMenuProfile;

    public GetMenuQueryHandlerTest()
    {
        _menuRepository = new();
        _getMenuProfile = new();
        var configuration = new MapperConfiguration(x => x.AddProfile(_getMenuProfile));
        _mapper = new Mapper(configuration);
        _unitOfWork = new();
    }

    [Fact]
    public async void GetMenu_Should_ReturnList()
    {
        // Arrange
        var query = new GetAllMenuQuery();

        var menuList = new List<Menu>();
        menuList.Add(Menu.Create("test1", "for testing"));

        _menuRepository.Setup(x => x.GetAsync()).Returns(Task.FromResult(menuList));
        var handler = new GetAllMenuQueryHandler(_mapper, _menuRepository.Object, _unitOfWork.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Data.Should().NotBeEmpty().And.NotBeNull();
        result.Succeeded.Should().BeTrue();
        result.Data.Should().BeOfType<List<GetAllMenuQueryResponse>>();
    }
}

