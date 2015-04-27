using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace EliaStore.Models
{

    public class db_configuration : DbConfiguration
    {
        public db_configuration()
        {
            //Instructs Entity Framework to use "dbcontext_initializer" class below
            // to pre-populate the database with inital set of data
            this.SetDatabaseInitializer<dbcontext>(new dbcontext_initializer());
            this.SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy(2, TimeSpan.FromSeconds(20)));
        }
    }

    [DbConfigurationType(typeof(db_configuration))]
    public class dbcontext : DbContext
    {
        public dbcontext()
            : base("clouddb")
        {
        }
        public DbSet<shirt> shirts { get; set; }

        public DbSet<shirtbrand> brands { get; set; }

    }

    //Input Entity Framework
    public class dbcontext_initializer : DropCreateDatabaseAlways<dbcontext>
    {
        private void InitializeCustomModels(dbcontext context)
        {
            // create makes and their models.
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


        }
    }
}