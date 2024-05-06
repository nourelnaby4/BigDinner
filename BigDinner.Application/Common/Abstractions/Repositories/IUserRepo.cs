namespace BigDinner.Application.Common.Abstractions.Repositories;

public interface IUserRepo : IBaseRepo<ApplicationUser>
{
    Task<ApplicationUser>? GetUserByEmail(string email);
    Task<ApplicationUser>? GetUserByUsername(string username);
}

