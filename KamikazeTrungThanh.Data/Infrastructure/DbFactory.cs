namespace KamikazeTrungThanh.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private KamikazeTrungThanhDbContext DbContext;

        public KamikazeTrungThanhDbContext Init()
        {
            return DbContext ?? (DbContext = new KamikazeTrungThanhDbContext());
        }

        protected override void DisposeCore()
        {
            if (DbContext != null)
                DbContext.Dispose();
        }
    }
}