using LosCasaAngular.Models;
using Microsoft.EntityFrameworkCore;


namespace LosCasaAngular.DAL;


public static class DBInit
{
    public static void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ListingDbContext context = serviceScope.ServiceProvider.GetRequiredService<ListingDbContext>();
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (!context.Listings.Any())
        {
            var listings = new List<Listing> { };
            context.AddRange(listings);
            context.SaveChanges();
        }

        if (!context.Customers.Any())
        {
            var customers = new List<Customer> { };
            context.AddRange(customers);
            context.SaveChanges();
        }

        if (!context.Rents.Any())
        {
            var rents = new List<Rent> { };
            context.AddRange(rents);
            context.SaveChanges();

        }

        if (!context.RentListings.Any())
        {
            var rentListings = new List<RentListing> { };
            foreach (var rentListing in rentListings)
            {
                var listing = context.Listings.Find(rentListing.ListingId);
                rentListing.RentListingPrice = rentListing.RentListingPrice;
            }
            context.AddRange(rentListings);
            context.SaveChanges();

        }

        var rentToUpdate = context.Rents.Include(o => o.RentListings);
        foreach (var rent in rentToUpdate)
        {
            rent.TotalPrice = rent.RentListings?.Sum(oi => oi.RentListingPrice) ?? 0;
        }
        context.SaveChanges();
    }
}
