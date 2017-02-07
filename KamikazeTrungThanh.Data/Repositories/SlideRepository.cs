using KamikazeTrungThanh.Data.Infrastructure;
using KamikazeTrungThanh.Model.Models;

namespace KamikazeTrungThanh.Data.Repositories
{
    public interface ISlideRepository : IRepository<Slide>
    {
    }

    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {
        public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}