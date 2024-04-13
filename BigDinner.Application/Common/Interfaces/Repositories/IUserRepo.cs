namespace BigDinner.Application.Common.Interfaces.Repositories;

public interface IUserRepo : IBaseRepo<ApplicationUser>
{
    Task<ApplicationUser>? GetUserByEmail(string email);
    Task<ApplicationUser>? GetUserByUsername(string username);
}

