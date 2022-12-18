using CoworkingBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoworkingBooking.Data.DbContexts
{
    public class CoworkingDBContext : DbContext
    {
        public CoworkingDBContext(DbContextOptions<CoworkingDBContext> options):base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; }

        public virtual DbSet<Floor> Floor { get; set; }

        public virtual DbSet<Table> Tables { get; set; }

        public virtual DbSet<Chair> Chairs { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
    }
}
