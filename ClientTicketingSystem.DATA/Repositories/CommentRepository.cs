using ClientTicketingSystem.CORE.Models;
using ClientTicketingSystem.DATA.Data;
using ClientTicketingSystem.DATA.Repositories.Interfaces;

namespace ClientTicketingSystem.DATA.Repositories;
public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context) { }

}