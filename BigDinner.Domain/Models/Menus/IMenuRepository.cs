namespace BigDinner.Domain.Models.Menus;

public interface IMenuRepository
{
    void Add(Menu menu);

    Task<List<Menu>> GetAsync();

    Task<Menu?> GetByIdAsync(Guid MenuId);

    Task<Menu?> GetMenuItemByIdAsync(Guid menuId,Guid menuItemId);

    void Update(Menu menu);
}
