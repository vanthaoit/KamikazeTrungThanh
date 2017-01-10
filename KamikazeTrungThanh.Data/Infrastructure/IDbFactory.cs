using System;

namespace KamikazeTrungThanh.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        KamikazeTrungThanhDbContext Init();
    }
}