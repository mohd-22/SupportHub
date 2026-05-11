using ClientTicketingSystem.CORE.Models;
using ClientTicketingSystem.DATA.Data;
using ClientTicketingSystem.DATA.Repositories.Interfaces;

namespace ClientTicketingSystem.DATA.Repositories;
public class UserRepository : GenericRepository<User>,IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

}