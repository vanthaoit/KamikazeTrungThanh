using KamikazeTrungThanh.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace KamikazeTrungThanh.Data
{
    public class KamikazeTrungThanhDbContext : IdentityDbContext<ApplicationUser>
    {
        public KamikazeTrungThanhDbContext() : base("KamikazeTrungThanhConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Footer> Footers { set; get; }
        public DbSet<Menu> Menus { set; get; }
        public DbSet<MenuGroup> MenuGroups { set; get; }
   
        
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }
        public DbSet<Slide> Slides { set; get; }

       
        public DbSet<Tag> Tags { set; get; }

        public DbSet<Error> Erros { set; get; }
        public DbSet<ContactDetail> ContactDetails { set; get; }
        public DbSet<Feedback> feedbacks { set; get; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(x => new { x.UserId, x.RoleId });
            builder.Entity<IdentityUserLogin>().HasKey(x => x.UserId);
        }

        public static KamikazeTrungThanhDbContext Create()
        {
            return new KamikazeTrungThanhDbContext();
        }
    }
}