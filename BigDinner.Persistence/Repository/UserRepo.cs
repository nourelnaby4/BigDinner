namespace BigDinner.Persistence.Repository;

public class UserRepo : BaseRepo<ApplicationUser>, IUserRepo
{
    private readonly DinnerDbContext _context;
    public UserRepo(DinnerDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task CreateUserAsync(ApplicationUser user, string password)
    {
        
    }

    public async Task<ApplicationUser>? GetUserByEmail(string email)
    {
       return await  _context.Users.Where(x=>x.Email == email).SingleOrDefaultAsync();
    }

    public async Task<ApplicationUser>? GetUserByUsername(string username)
    {
        return await _context.Users.Where(x => x.UserName == username).SingleOrDefaultAsync();
    }
}

