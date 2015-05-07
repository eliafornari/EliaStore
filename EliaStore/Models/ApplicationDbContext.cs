using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using EliaStore.Models;
using EliaStore;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace EliaStore.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("clouddb")
        {
        }

        static ApplicationDbContext()
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        public DbSet<shirt> shirts { get; set; }

        public DbSet<shirtbrand> brands { get; set; }

    }

    public class db_configuration : DbConfiguration
    {
        public db_configuration()
        {
            //Instructs Entity Framework to use "dbcontext_initializer" class below
            // to pre-populate the database with inital set of data
            this.SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy(2, TimeSpan.FromSeconds(20)));
        }
    }

    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            InitializeCustomModels(context);
            base.Seed(context);
        }

        private void InitializeCustomModels(ApplicationDbContext context)
        {
            var Elia_brand = new shirtbrand { name = "Elia" };
            Elia_brand.models.Add(new shirtmodel { name = "one" });
            Elia_brand.models.Add(new shirtmodel { name = "two" });
            Elia_brand.models.Add(new shirtmodel { name = "three" });
            Elia_brand.models.Add(new shirtmodel { name = "four" });
            Elia_brand.models.Add(new shirtmodel { name = "four" });

            var Qual_brand = new shirtbrand { name = "QualSquad" };
            Qual_brand.models.Add(new shirtmodel { name = "long_sleeve" });

            var BeenTrill_brand = new shirtbrand { name = "BeenTrill" };
            BeenTrill_brand.models.Add(new shirtmodel { name = "sweater" });

            // add newly created makes to .makes dbset defined in dbcontext
            context.brands.Add(Elia_brand);
            context.brands.Add(Qual_brand);
            context.brands.Add(BeenTrill_brand);


            // persist changes to the database
            context.SaveChanges();

            var one_model = Elia_brand.models.Single(m => m.name == "one");
            context.shirts.Add(new shirt
            {
                brand = Elia_brand,
                model = one_model,
                price = 30,
                color = shirt_color.Black,
                img_url = @"../Images/one.png",
                big_img_url = @"../Images/big_one.png"
            });

            context.SaveChanges();
        }

        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(db));
            const string name = "admin@example.com";
            const string password = "Admin@123456";
            string roleName = "User";

            //Create Role Admin if it does not exist

            var role = AddRole(roleManager, roleName);
            roleName = "Admin";
            role = AddRole(roleManager, roleName);

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }

        private static IdentityRole AddRole(ApplicationRoleManager roleManager, string roleName)
        {
            var role = roleManager.FindByName(roleName);
            if (role==null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
                return role;
            }
            return role;
        }
    }
}