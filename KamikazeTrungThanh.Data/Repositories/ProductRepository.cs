using KamikazeTrungThanh.Data.Infrastructure;
using KamikazeTrungThanh.Model.Models;

namespace KamikazeTrungThanh.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}