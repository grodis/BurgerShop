namespace beeftechee.Migrations
{
    using beeftechee.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<beeftechee.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "beeftechee.Models.ApplicationDbContext";
        }

        protected override void Seed(beeftechee.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };


                manager.Create(role);


            }
            if (!context.Roles.Any(r => r.Name == "Supervisor"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Supervisor" };


                manager.Create(role);

            }
            //    if (!context.Roles.Any(r => r.Name == "User"))
            //    {
            //        var store = new RoleStore<IdentityRole>(context);
            //        var manager = new RoleManager<IdentityRole>(store);
            //        var role = new IdentityRole { Name = "User" };


            //        manager.Create(role);

            //    }


            var PasswordHash = new PasswordHasher();


            if (!context.Users.Any(u => u.UserName == "admin@admin.net"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "admin@admin.net",
                    Email = "admin@admin.net",

                    PasswordHash = PasswordHash.HashPassword("Grodis1!")
                };

                manager.Create(user);
                manager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
