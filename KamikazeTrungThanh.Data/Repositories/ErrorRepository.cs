﻿using KamikazeTrungThanh.Data.Infrastructure;
using KamikazeTrungThanh.Model.Models;

namespace KamikazeTrungThanh.Data.Repositories
{
    public interface IErrorRepository : IRepository<Error>
    {
    }

    public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}