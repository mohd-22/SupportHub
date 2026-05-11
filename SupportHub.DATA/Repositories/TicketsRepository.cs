using SupportHub.CORE.Models;
using SupportHub.DATA.Data;
using SupportHub.DATA.Repositories.Interfaces;

namespace SupportHub.DATA.Repositories;
public class TicketsRepository : GenericRepository<Ticket>,ITicketRepository
{
    public TicketsRepository(AppDbContext context) : base(context) { }

}