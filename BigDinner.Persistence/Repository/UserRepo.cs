namespace BigDinner.Persistence.Repository;

public class UserRepo : BaseRepo<ApplicationUser>, IUserRepo
{
    public UserRepo(ApplicationDbContext context) : base(context)
    {
    }
}

