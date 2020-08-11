namespace Ninhao.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class NinhaoContext : DbContext
    {
        // Your context has been configured to use a 'NinhaoContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Ninhao.Models.NinhaoContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'NinhaoContext' 
        // connection string in the application configuration file.
        public NinhaoContext()
            : base("NinhaoContext")
        {
            Database.SetInitializer<NinhaoContext>(strategy: null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}