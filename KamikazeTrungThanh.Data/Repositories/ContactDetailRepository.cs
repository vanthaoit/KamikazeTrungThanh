using KamikazeTrungThanh.Data.Infrastructure;
using KamikazeTrungThanh.Model.Models;

namespace KamikazeTrungThanh.Data.Repositories
{
    public interface IContactDetailRepository : IRepository<ContactDetail>
    {
    }

    public class ContactDetailRepository : RepositoryBase<ContactDetail>, IContactDetailRepository
    {
        public ContactDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}