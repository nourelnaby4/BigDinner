namespace BigDinner.Domain.Models.Menus;

public interface IMenuRepository
{
    void Add(MenuCategory menu);
    IEnumerable<MenuCategory> GetAll();
    MenuCategory GetById();
}
