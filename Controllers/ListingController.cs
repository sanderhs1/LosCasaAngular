using LosCasaAngular.DAL;
using LosCasaAngular.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LosCasaAngular.Controllers;

private readonly ILogger<ListingController> _logger;
private readonly InterListingRepository _listingRepository;
public ListingController(InterListingRepository listingRepository, ILogger<ListingController> logger)
{
    _logger = logger;
    _listingRepository = listingRepository;
}

//public List<Rent> RentConsole()
//{
//    return ListingRepository.Rents.ToList();

//}


public async Task<IActionResult> Table(decimal? maxPrice = null, int? minRooms = null)
{
    // Retrieve listings from repository
    IQueryable<Listing> query = _listingRepository.GetAllAsQueryable();

    // If max price is provided it will filter listings on max.Price
    if (maxPrice.HasValue)
    {
        query = query.Where(l => l.Price <= maxPrice.Value);
    }

    // If min is provided it will filter listings on min.Price
    if (minRooms.HasValue)
    {
        query = query.Where(l => l.AntallRom >= minRooms.Value);
    }

    // Execute the query and retrieve the result as list
    var listings = await query.ToListAsync();
    var listingListViewModel = new ListingListViewModel(listings, "Table");

    // If no listing is found the listing it will log the error and send the error
    if (listings == null || !listings.Any())
    {
        _logger.LogError("[ListingController] No listings found with the specified filters.");

        listingListViewModel.ErrorMessage = "No listings found with the specified filters.";
    }

    // Returns the view with the view model data

    return View(listingListViewModel);
}

// Displays the listings in grid
public async Task<IActionResult> Grid()
{

    // Retrieves the listings from the repository
    var listings = await _listingRepository.GetAll();

    // If no listing are found it will log the error and send the error
    if (listings == null)
    {
        _logger.LogError("[ListingController] Listings list not found while executing _listingRepository.GetAll()");
        return NotFound("Listings list not found");
    }

    // Create a view model for the listings
    var listingListViewModel = new ListingListViewModel(listings, "Grid");
    return View(listingListViewModel);
}

public async Task<IActionResult> Details(int id)
{

    // Retrieve the listing with the specified Id
    var listing = await _listingRepository.GetListingById(id);

    // If listing is not found it will logg the error and send the error. 
    if (listing == null)
    {
        _logger.LogError("[ListingController] Listing not found for the ListingId {ListingId: 0000}", id);
        return NotFound("Listings list not found for the ListingId");
    }

    return View(listing);
}

[Authorize]
[HttpGet]
public IActionResult Create()
{
    return View();
}


// This method handles Post Request to create a new listing

[Authorize]
[HttpPost]
public async Task<IActionResult> Create(Listing listing)
{
    // Check if the provided listing data is valid
    if (ModelState.IsValid)
    {
        // Ensre the listing price is greater than 0 
        if (listing.Price <= 0)
        {
            ModelState.AddModelError("PricePerNight", "Price per night must be more than 0.");
            // Rerturn to the create view with error and data
            return View(listing);
        }

        // Create listing in repository
        bool returnOk = await _listingRepository.Create(listing);
        if (returnOk)
            return RedirectToAction(nameof(Table));
    }

    // If the listing creation failed, it logs the warning with details
    _logger.LogWarning("[LisitngController] Listing creation failed {@listing}", listing);
    return View(listing);
}

[Authorize]
[HttpGet]
public async Task<IActionResult> Update(int id)
{
    // Retrieve the listing with specified ID from repository
    var listing = await _listingRepository.GetListingById(id);

    // If the listing with the specified ID is not found, it logs error, return "Bad Request"
    if (listing == null)
    {
        _logger.LogError("[ListingController] Listing not found when updating the ListingId{ListingId:0000}", id);
        return BadRequest("Listing not found for the ListingId");
    }
    return View(listing);
}

[Authorize]
[HttpPost]
public async Task<IActionResult> Update(Listing listing)
{
    // Check if the listing data is valid
    if (ModelState.IsValid)
    {

        // Update the listing in repository
        bool returnOk = await _listingRepository.Update(listing);
        // If its successfull it will redirect to Table View
        if (returnOk)
            return RedirectToAction(nameof(Table));
    }
    // If the update on listing is failed it will log the warning with the listing details 
    _logger.LogWarning("[ListingController] Listing update failed {@listing}", listing);

    // Return to Update view with provided listing.
    return View(listing);
}

[Authorize]
[HttpGet]
public async Task<IActionResult> Delete(int id)
{

    // Retrieve the listing with the specified Id
    var listing = await _listingRepository.GetListingById(id);

    // If the listing with the specified ID is not found it will log the error.
    if (listing == null)
    {
        _logger.LogError("[ListingController] Listing not found for the ListingId {ListingId:0000}", listing);
        return BadRequest("Listing not found for the ListingId");
    }
    return View(listing);
}

[Authorize]
[HttpPost]
public async Task<IActionResult> DeleteConfirmed(int id)
{

    // It will delete the listing with the specified id in the repository
    bool returnOk = await _listingRepository.Delete(id);

    // If the deletion of the listing fails it will log the error and send a Bad Request.
    if (!returnOk)
    {
        _logger.LogError("[ListingController] Listing deletion failed for the ListingId {ListingId:0000}", id);
        return BadRequest("Listing deletion failed");
    }

    // If it is successfull it will redirect to Table
    return RedirectToAction(nameof(Table));
}
[Authorize]
[HttpGet]
public async Task<IActionResult> ListingView(int id)
{

    // Retrieve the listing with the specified Id from the repository
    var listing = await _listingRepository.GetListingById(id);

    // If the listing with the specified Id is not found it will log an error and send Bad Request.
    if (listing == null)
    {
        _logger.LogError("[ListingController] Listing not found for the ListingId {ListingId:0000}", listing);
        return BadRequest("Listing not found for the ListingId");
    }

    // Retrieve all the listings and rents

    var listings = await _listingRepository.GetAllListings();
    var rents = await _listingRepository.GetAllRents();

    var viewModel = new ListingWithRentVM
    {
        ListingDetails = listing,
        RentDetails = new CreateRentListingViewModel
        {
            RentListing = new RentListing { ListingId = listing.ListingId },
            ListingSelectList = listings.Select(l => new SelectListItem
            {
                Value = l.ListingId.ToString(),
                Text = $"{l.ListingId}: {l.Name}"
            }).ToList(),
            RentSelectList = rents.Select(r => new SelectListItem
            {
                Value = r.RentId.ToString(),
                Text = $"Rent {r.RentId}"
            }).ToList()
        }
    };
    // Return the " Listing View" with the view model
    return View(viewModel);
}
}





