
using LosCasaAngular.DAL;
using LosCasaAngular.Models;

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
            var listings = new List<Listing>
            {
                new Listing
                {
                    Name = "Pizza",
                    Price = 150,
                    Description = "Delicious Italian dish with a thin crust topped with tomato sauce, cheese, and various toppings.",
                    ImageUrl = "assets/images/pizza.jpg"
                },
                new Listing
                {
                    Name = "Fried Chicken Leg",
                    Price = 20,
                    Description = "Crispy and succulent chicken leg that is deep-fried to perfection, often served as a popular fast food item.",
                    ImageUrl = "assets/images/chickenleg.jpg"
                },
                new Listing
                {
                    Name = "French Fries",
                    Price = 50,
                    Description = "Crispy, golden-brown potato slices seasoned with salt and often served as a popular side dish or snack.",
                    ImageUrl = "assets/images/frenchfries.jpg"
                },
                new Listing
                {
                    Name = "Grilled Ribs",
                    Price = 250,
                    Description = "Tender and flavorful ribs grilled to perfection, usually served with barbecue sauce.",
                    ImageUrl = "assets/images/ribs.jpg"
                },
                new Listing
                {
                    Name = "Tacos",
                    Price = 150,
                    Description = "Tortillas filled with various ingredients such as seasoned meat, vegetables, and salsa, folded into a delicious handheld meal.",
                    ImageUrl = "assets/images/tacos.jpg"
                },
                new Listing
                {
                    Name = "Fish and Chips",
                    Price = 180,
                    Description = "Classic British dish featuring battered and deep-fried fish served with thick-cut fried potatoes.",
                    ImageUrl = "assets/images/fishandchips.jpg"
                },
                new Listing
                {
                    Name = "Cider",
                    Price = 50,
                    Description = "Refreshing alcoholic beverage made from fermented apple juice, available in various flavors.",
                    ImageUrl = "assets/images/cider.jpg"
                },
                new Listing 
                {
                    Name = "Coke",
                    Price = 30,
                    Description = "Popular carbonated soft drink known for its sweet and refreshing taste.",
                    ImageUrl = "assets/images/coke.jpg"
                },
            };
            context.AddRange(listings);
            context.SaveChanges();
        }

    }
}
