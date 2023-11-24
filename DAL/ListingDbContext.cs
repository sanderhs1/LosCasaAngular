using System;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using LosCasaAngular.Models;
using static System.Net.Mime.MediaTypeNames;

namespace LosCasaAngular.DAL;

public class ListingDbContext : DbContext
{
    public ListingDbContext(DbContextOptions<ListingDbContext> options) : base(options)
    {
    }

    public DbSet<Listing> Listings { get; set; }
    //public DbSet<Customer> Customers { get; set; }
    //public DbSet<Rent> Rents { get; set; }
    //public DbSet<RentListing> RentListings { get; set; }
    //public DbSet<RentListing> StartDate { get; set; }
    //public DbSet<RentListing> EndDate { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}