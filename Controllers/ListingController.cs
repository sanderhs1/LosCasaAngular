using Microsoft.AspNetCore.Mvc;
using LosCasaAngular.Models;
using LosCasaAngular.DAL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LosCasaAngular.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingController : Controller
{
    private readonly InterListingRepository _listingRepository;
    private readonly ILogger<ListingController> _logger;

    public ListingController(InterListingRepository listingRepository, ILogger<ListingController> logger)
    {
        _listingRepository = listingRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var listings = await _listingRepository.GetAll();
        if (listings == null)
        {
            _logger.LogError("[ListingController] Listing list not found while executing _listingRepository.GetAll()");
            return NotFound("Listing list not found");
        }
        return Ok(listings);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] Listing newListing)
    {
        if (newListing == null)
        {
            return BadRequest("Invalid item data.");
        }
        bool returnOk = await _listingRepository.Create(newListing);

        if (returnOk)
        {
            var response = new { success = true, message = "Listing " + newListing.Name + " created successfully" };
            return Ok(response);
        }
        else
        {
            var response = new { success = false, message = "Listing creation failed" };
            return Ok(response);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetListingbyId(int id)
    {
        var listing = await _listingRepository.GetListingById(id);
        if (listing == null)
        {
            _logger.LogError("[ItemController] Item list not found while executing _itemRepository.GetAll()");
            return NotFound("Item list not found");
        }
        return Ok(listing);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(Listing newListing)
    {
        if (newListing == null)
        {
            return BadRequest("Invalid listing data.");
        }
        bool returnOk = await _listingRepository.Update(newListing);
        if (returnOk)
        {
            var response = new { success = true, message = "Listing " + newListing.Name + " updated successfully" };
            return Ok(response);
        }
        else
        {
            _logger.LogError("[ListingController] Listing update failed for the Listing " + newListing.Name);
            var response = new { success = false, message = "Listing creation failed" };
            return Ok(response);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteListing(int id)
    {
        bool returnOk = await _listingRepository.Delete(id);
        if (!returnOk)
        {
            _logger.LogError("[ListingController] Item deletion failed for the ListingId {ListingId:0000}", id);
            return BadRequest("Listing deletion failed");
        }
        var response = new { success = true, message = "Listing " + id.ToString() + " deleted successfully" };
        return Ok(response);
    }
}

