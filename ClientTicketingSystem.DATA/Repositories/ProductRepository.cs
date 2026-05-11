using ClientTicketingSystem.CORE.Models;
using ClientTicketingSystem.DATA.Data;
using ClientTicketingSystem.DATA.Repositories.Interfaces;

namespace ClientTicketingSystem.DATA.Repositories;
public class ProductRepository : GenericRepository<Product>,IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

}