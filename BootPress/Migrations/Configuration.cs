namespace BootPress.Migrations
{
    using BootPress.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BootPress.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BootPress.Models.ApplicationDbContext context)
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
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new IdentityRole { Name = "Author" });
                roleManager.Create(new IdentityRole { Name = "Administrator" });
            };
            var user1 = new ApplicationUser
            {
                PhoneNumber = "+62818271214",
                PhoneNumberConfirmed = true,
                UserName = "user1@gmail.com",
                Email = "user1@gmail.com",
                EmailConfirmed = true,
                FullName = "User Satu",
                Registered = DateTimeOffset.Now,
                IsBanned = false
            };
            if (manager.FindByName("user1@gmail.com") == null)
            {
                manager.Create(user1, "Bootcamp#2017!");
                manager.AddToRoles(user1.Id, "Author");
            };
            var user2 = new ApplicationUser
            {
                PhoneNumber = "+62818271214",
                PhoneNumberConfirmed = true,
                UserName = "user2@gmail.com",
                Email = "user2@gmail.com",
                EmailConfirmed = true,
                FullName = "User DUa",
                Registered = DateTimeOffset.Now,
                IsBanned = false
            };
            if (manager.FindByName("user2@gmail.com") == null)
            {
                manager.Create(user2, "Bootcamp#2017!");
                manager.AddToRoles(user2.Id, "Author");
            };
            var admin = new Staff
            {
                PhoneNumber = "+62818271214",
                PhoneNumberConfirmed = true,
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                FullName = "Administrator",
                Registered = DateTimeOffset.Now,
                Department = "IT",
                Started = DateTimeOffset.Now,
                IsBanned = false
            };
            if (manager.FindByName("admin@gmail.com") == null)
            {
                manager.Create(admin, "Bootcamp#2017!");
                manager.AddToRoles(admin.Id, "Administrator");
            };
        }
    }
}
