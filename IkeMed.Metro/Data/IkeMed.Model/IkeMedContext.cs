using IkeMed.Model.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IkeCode.Core.Log;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;

namespace IkeMed.Model
{
    public class IkeMedContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<NaturalPerson> NaturalPeople { get; set; }
        public DbSet<LegalPerson> LegalPeople { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Document> Documents { get; set; }

        public IkeMedContext()
            : base("IkeMed")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<IkeMedContext, Configuration>());

            modelBuilder.Entity<Person>()
                .HasOptional<Doctor>(f => f.Doctor)
                .WithRequired(r => r.Person)
                .Map(m => m.MapKey("PersonId"));

            modelBuilder.Entity<Person>()
                .HasOptional<NaturalPerson>(f => f.NaturalPerson)
                .WithRequired(r => r.Person)
                .Map(m => m.MapKey("PersonId"));

            modelBuilder.Entity<Person>()
                .HasOptional<LegalPerson>(f => f.LegalPerson)
                .WithRequired(r => r.Person)
                .Map(m => m.MapKey("PersonId"));

            modelBuilder.Entity<Person>()
                .HasMany<Address>(f => f.Addresses)
                .WithRequired(r => r.Person)
                .Map(m => m.MapKey("PersonId"));

            modelBuilder.Entity<Person>()
                .HasMany<Phone>(f => f.Phones)
                .WithRequired(r => r.Person)
                .Map(m => m.MapKey("PersonId"));

            modelBuilder.Entity<Person>()
                .HasMany<Document>(f => f.Documents)
                .WithRequired(r => r.Person)
                .Map(m => m.MapKey("PersonId"));

            modelBuilder.Entity<Document>()
                .HasRequired<DocumentType>(f => f.DocumentType)
                .WithMany(m => m.Documents)
                .Map(m => m.MapKey("DocumentTypeId"));
        }

        public override int SaveChanges()
        {
            var result = 0;
            try
            {
                var modifiedEntries = this.ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified).ToList();

                foreach (var dbEntityEntry in modifiedEntries)
                {
                    if ((DateTime)dbEntityEntry.Property("DateIns").CurrentValue == DateTime.MinValue
                        || dbEntityEntry.State == EntityState.Added)
                    {
                        dbEntityEntry.Property("DateIns").CurrentValue = DateTime.Now;
                    }

                    dbEntityEntry.Property("LastUpdate").CurrentValue = DateTime.Now;
                }

                result = base.SaveChanges();
            }
            catch (DbEntityValidationException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (UpdateException)
            {
                throw;
            }
            catch (Exception e)
            {
                IkeCodeLog.Default.Exception(e);
                throw;
            }

            return result;
        }
    }
}
