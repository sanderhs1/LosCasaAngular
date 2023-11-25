using Microsoft.EntityFrameworkCore;
using LosCasaAngular.Models;

namespace LosCasaAngular.DAL;

public class ListingDbContext : DbContext
{
    public ListingDbContext(DbContextOptions<ListingDbContext> options) : base(options)
    {
    }

    public DbSet<Listing> Listings{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}

