using SupportHub.CORE.Models;
using SupportHub.DATA.Data;
using SupportHub.DATA.Repositories.Interfaces;

namespace SupportHub.DATA.Repositories;
public class ProductRepository : GenericRepository<Product>,IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

}