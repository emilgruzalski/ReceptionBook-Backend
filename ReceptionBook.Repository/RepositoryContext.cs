using Microsoft.EntityFrameworkCore;
using ReceptionBook.Entities.Models;
using ReceptionBook.Repository.Configuration;

namespace ReceptionBook.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new MaintenanceConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        }

        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Maintenance>? Maintenance { get; set; }
        public DbSet<Reservation>? Reservations { get; set; }
        public DbSet<Room>? Rooms { get; set; }

    }
}
