using Microsoft.EntityFrameworkCore;
using Parkfinder.Models;


namespace Parkfinder.Context
{
    public class PFDBContext : DbContext
    {
        public PFDBContext(DbContextOptions<PFDBContext> options) : base(options)
        {

        }

        public DbSet<Parkeringsområde> Parkeringsområde { get; set; }

    }
}
