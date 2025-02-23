using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace HotelRv.Infrastructure.Persistence.Context
{
    public class HotelVRContext: DbContext
    {

        public const String DEFAULT_SCHEMA = "dbo";

        public HotelVRContext()
        {
            
        }

        public HotelVRContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
        public DbSet<Hotel> hotels { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<Reservation>  reservations { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<Room> rooms { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<District> districts { get; set; }
        
        public DbSet<SupportTicket>  supportTickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connStr = "Server=YUSUF\\YUSUF;Database=HotelRVDb;User Id=sa;Password=password1;TrustServerCertificate=True; Encrypt=false";
            optionsBuilder.UseSqlServer(connStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public override int SaveChanges()
        {
            OnBeforeSave();
            return base.SaveChanges();
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);

        }

        private void OnBeforeSave()
        {
            var addedEntities = ChangeTracker.Entries()
                                              .Where(i => i.State == EntityState.Added)
                                              .Select(i => (BaseEntity)i.Entity);
            PrepareAddedEntities(addedEntities);
        }

        private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.CreateDate == DateTime.MinValue)
                    entity.CreateDate = DateTime.Now;
            }
        }


    }
}
