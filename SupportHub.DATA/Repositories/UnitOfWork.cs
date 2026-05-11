using SupportHub.CORE.Models;
using SupportHub.DATA.Data;
using SupportHub.DATA.Repositories.Interfaces;

namespace SupportHub.DATA.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; }
    public IGenericRepository<Attachment> Attachments { get; }
    public ICommentRepository Comments { get; }
    public IGenericRepository<ProductModule> ProdectModules { get; }
    public IProductRepository Products { get; }
    public ITicketRepository Tickets { get; }


    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Attachments = new GenericRepository<Attachment>(_context);
        Comments = new CommentRepository(_context);
        ProdectModules = new GenericRepository<ProductModule>(_context);
        Products = new ProductRepository(_context);
        Tickets = new TicketsRepository(_context);
    }
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
