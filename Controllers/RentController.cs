using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Forum.DAL;
using Forum.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.Xml;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;


namespace Forum.Controllers
{
    public class RentController : Controller
    {
        private readonly ListingDbContext _listingDbContext;
        private readonly ILogger<RentController> _logger;

        public RentController(ListingDbContext listingDbContext, ILogger<RentController> logger)
        {
            _listingDbContext = listingDbContext;
            _logger = logger;
        }
        public async Task<IActionResult> Table()
        {
            List<Rent> rents = await _listingDbContext.Rents.ToListAsync();
            if (rents == null || !rents.Any())
            {
                _logger.LogError("[RentController] No rents found.");
            }
            return View(rents);
        }
        [HttpGet]
        public async Task<IActionResult> RentDetails(int rentId)
        {
            var rent = await _listingDbContext.Rents
                .Include(r => r.RentListings)
                .ThenInclude(rl => rl.Listing)
                .FirstOrDefaultAsync(r => r.RentId == rentId);

            if (rent == null)
            {
                _logger.LogWarning("[RentController] Rent not found for RentId {RentId}", rentId);
                return NotFound();
            }

            var viewModel = new RentDetailsViewModel
            {
                Rent = rent,
                RentListings = rent.RentListings,
                Listing = rent.RentListings.FirstOrDefault()?.Listing
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpGet]

        public async Task<IActionResult> CreateRentListing(int ListingId)
        {
            var listings = await _listingDbContext.Listings.ToListAsync();
            var rents = await _listingDbContext.Rents.ToListAsync();
            var createRentListingViewModel = new CreateRentListingViewModel
            {
                RentListing = new RentListing { ListingId = ListingId },

                ListingSelectList = listings.Select(listing => new SelectListItem
                {
                    Value = listing.ListingId.ToString(),
                    Text = listing.ListingId.ToString() + ": " + listing.Name
                }).ToList(),

                RentSelectList = rents.Select(rent => new SelectListItem
                {
                    Value = rent.RentId.ToString(),
                    Text = "Rent" + rent.RentId.ToString() + ", Customer: " + rent.Customer?.CustomerName ?? "Customer Not Found"
                }).ToList(),

            };
            return View(createRentListingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRentListing(RentListing rentListing)
        {
            if (rentListing.EndDate <= rentListing.StartDate)
            {
                _logger.LogWarning("[RentController] End date {EndDate} is not greater than start date {StartDate}", rentListing.EndDate, rentListing.StartDate);
                var rentDetailsViewModel = new RentDetailsViewModel
                {

                    ErrorMessage = "End date must be greater than start date."
                };


                return View("RentDetails", rentDetailsViewModel);
            }
            try
            {
                var newListing = await _listingDbContext.Listings.FindAsync(rentListing.ListingId);
                var newRent = await _listingDbContext.Rents.FindAsync(rentListing.RentId);
                _logger.LogInformation("[RentController] RentListing created successfully for ListingId {ListingId} and RentId {RentId}", rentListing.ListingId, rentListing.RentId);


                if (newListing == null || newRent == null)
                {
                    return BadRequest("Listing or Rent not found");
                }

                var newRentListing = new RentListing
                {
                    ListingId = rentListing.ListingId,
                    Listing = newListing,
                    RentId = rentListing.RentId,
                    StartDate = rentListing.StartDate,
                    EndDate = rentListing.EndDate,
                    Rent = newRent
                };


                int daysStayed = (newRentListing.EndDate - newRentListing.StartDate).Days;


                newRentListing.RentListingPrice = daysStayed * newRentListing.Listing.Price;

                if (newRentListing.Listing == null || newRentListing.Rent == null)
                {
                    var listings = await _listingDbContext.Listings.ToListAsync();
                    var rents = await _listingDbContext.Rents.ToListAsync();
                    var createRentListingViewModel = new CreateRentListingViewModel
                    {
                        RentListing = rentListing,
                        ListingSelectList = listings.Select(listing => new SelectListItem
                        {
                            Value = listing.ListingId.ToString(),
                            Text = listing.ListingId.ToString() + ": " + listing.Name
                        }).ToList(),

                        RentSelectList = rents.Select(rent => new SelectListItem
                        {
                            Value = rent.RentId.ToString(),
                            Text = "Rent" + rent.RentId.ToString() + ", Customer: " + rent.Customer?.CustomerName ?? "Cusotmer not Found"
                        }).ToList(),
                    };
                    return View(createRentListingViewModel);
                }
                _listingDbContext.RentListings.Add(newRentListing);
                await _listingDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(RentDetails), new { rentId = newRentListing.RentId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[RentController] RentListing creation failed due to an exception.");

                return BadRequest("RentListing creation failed.");
            }
        }
    }
}