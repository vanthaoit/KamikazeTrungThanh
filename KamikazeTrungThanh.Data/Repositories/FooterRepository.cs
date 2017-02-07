using KamikazeTrungThanh.Data.Infrastructure;
using KamikazeTrungThanh.Model.Models;

namespace KamikazeTrungThanh.Data.Repositories
{
    public interface IFooterRepository : IRepository<Footer>
    {
    }

    public class FooterRepository : RepositoryBase<Footer>, IFooterRepository
    {
        public FooterRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}