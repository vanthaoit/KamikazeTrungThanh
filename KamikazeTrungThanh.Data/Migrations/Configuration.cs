namespace KamikazeTrungThanh.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KamikazeTrungThanh.Data.KamikazeTrungThanhDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KamikazeTrungThanh.Data.KamikazeTrungThanhDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //CreateContactDetail(context);
            //CreateUser(context);
        }

        private void CreateUser(KamikazeTrungThanhDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new KamikazeTrungThanhDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new KamikazeTrungThanhDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "KamikazeTrungThanh",
                Email = "vietnamthaotranvan@gmail.com",
                EmailConfirmed = true,
                //BirthDay = DateTime.Now,
                //FullName = " Administrator"
            };

            //manager.Create(user, "123456");
            manager.Create(user, "adminktt@123456");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                //roleManager.Create(new IdentityRole { Name = "User" });
            }

            //var adminUser = manager.FindByEmail("vietnamthaotranvan@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateContactDetail(KamikazeTrungThanhDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                try
                {
                    var contactDetail = new KamikazeTrungThanh.Model.Models.ContactDetail()
                    {
                        Name = "Công ty TNHH Trung Thanh",
                        Phone = "0908.999.623",
                        Address = "C5/2 ấp 3 Xã Vĩnh Lộc A, H.Bình Chánh, TP.HCM",
                        Lat = 10.831988,
                        Lng = 106.5636096,
                        Other = "",
                        Status = true
                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }
            }
        }
    }
}
