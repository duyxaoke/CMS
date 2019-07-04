using CMS.Domain.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace CMS.Infra.Data.Context
{
    public class SampleContext : DbContext
    {
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuInRole> MenuInRole { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryMapping> CategoryMapping { get; set; }
        public DbSet<Config> Config { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<ContentMapping> ContentMapping { get; set; }

        public SampleContext() : base("name=DefaultConnection")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SampleContext, DatabaseInitializer>());//initial database use test data            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("nvarchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(200));
            base.OnModelCreating(modelBuilder);
        }

        //public override int SaveChanges()
        //{
        //    foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        //    {
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Property("DataCadastro").CurrentValue = DateTime.Now;
        //        }

        //        if (entry.State == EntityState.Modified)
        //        {
        //            entry.Property("DataCadastro").IsModified = false;
        //        }
        //    }
        //    return base.SaveChanges();
        //}
    }
}
