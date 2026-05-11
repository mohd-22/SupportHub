using ClientTicketingSystem.CORE.Models;
using ClientTicketingSystem.DATA.Data;
using ClientTicketingSystem.DATA.Repositories.Interfaces;

namespace ClientTicketingSystem.DATA.Repositories;
public class TicketsRepository : GenericRepository<Ticket>,ITicketRepository
{
    public TicketsRepository(AppDbContext context) : base(context) { }

}