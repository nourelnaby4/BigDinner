namespace BigDinner.Domain.Models.Menus;

public interface IMenuRepository
{
    void Add(Menu menu);
    Task<IEnumerable<Menu>> GetAll();
    Task<Menu?> GetById(Guid MenuId);
}
