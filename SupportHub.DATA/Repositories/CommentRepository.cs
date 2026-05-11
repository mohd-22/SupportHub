using SupportHub.CORE.Models;
using SupportHub.DATA.Data;
using SupportHub.DATA.Repositories.Interfaces;

namespace SupportHub.DATA.Repositories;
public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context) { }

}