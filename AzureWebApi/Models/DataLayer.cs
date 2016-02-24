using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace WebCatalogue.Models
{
    public class MyDbInitializer : CreateDatabaseIfNotExists<EFContext>
    {
        public override void InitializeDatabase(EFContext context)
        {
            // Add command timeout 600
            context.Database.CommandTimeout = 600;
            base.InitializeDatabase(context);
        }
    }

    public class SQLAzureConfiguration : DbConfiguration
    {
        public SQLAzureConfiguration()
        {
            this.SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }

    [DbConfigurationType(typeof(SQLAzureConfiguration))]
    public class EFContext : DbContext
    {
        public EFContext() : base("DBConnection") { }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubCategory> ProductSubCategories { get; set; }
    }

}
