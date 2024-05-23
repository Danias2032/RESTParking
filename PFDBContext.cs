using Microsoft.EntityFrameworkCore;


namespace Parkfinder
{
    public class PFDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public PFDBContext(DbContextOptions<PFDBContext> options) : base(options)
        {

        }

        public Microsoft.EntityFrameworkCore.DbSet<Parkeringsområde> Parkeringsområde { get; set; }

    }
}
