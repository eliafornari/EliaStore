using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EliaStore.Models;

namespace EliaStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
            //System.Data.Entity.DbConfiguration.SetConfiguration(new db_configuration());
            //Database.SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy(2, TimeSpan.FromSeconds(20)));
        }
    }
}