using Microsoft.AspNetCore.Mvc;
using LosCasaAngular.Models;
using LosCasaAngular.DAL;
using System;
using Microsoft.Extensions.Logging;

namespace LosCasaAngular.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentController : Controller
{
    private readonly InterListingRepository _listingRepository;
    private readonly ILogger<RentController> _logger;

    public RentController(InterListingRepository listingRepository, ILogger<RentController> logger)
    {
        _listingRepository = listingRepository;
        _logger = logger;
    }

    // Get all rents
    [HttpGet]
    public async Task<IActionResult> GetAllRents()
    {
        try
        {
            var rents = await _listingRepository.GetAllRents();
            if (rents == null)
            {
                _logger.LogError("[RentController] Rent list not found.");
                return NotFound("Rent list not found.");
            }
            return Ok(rents);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[RentController] Error occurred while getting all rents.");
            return StatusCode(500, "Internal server error while getting rents.");
        }
    }

    // Get rent by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRentById(int id)
    {
        try
        {
            var rent = await _listingRepository.GetRentById(id);
            if (rent == null)
            {
                _logger.LogError($"[RentController] Rent not found for ID: {id}");
                return NotFound($"Rent not found for ID: {id}");
            }
            return Ok(rent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[RentController] Error occurred while getting rent by ID.");
            return StatusCode(500, "Internal server error while getting rent by ID.");
        }
    }

    // Create a new rent
    [HttpPost]
    public async Task<IActionResult> CreateRent([FromBody] Rent rent)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRent = await _listingRepository.CreateRent(rent);
            if (createdRent == null)
            {
                _logger.LogError("[RentController] Rent could not be created.");
                return BadRequest("Rent could not be created.");
            }
            return CreatedAtAction(nameof(GetRentById), createdRent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[RentController] Error occurred while creating rent.");
            return StatusCode(500, "Internal server error while creating rent.");
        }
    }

    // Update an existing rent
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRent(int id, [FromBody] Rent rent)
    {
        try
        {
            if (id != rent.RentId)
            {
                return BadRequest("Mismatched rent ID.");
            }

            var updatedRent = await _listingRepository.UpdateRent(rent);
            if (updatedRent == null)
            {
                _logger.LogError($"[RentController] Rent could not be updated for ID: {id}");
                return NotFound($"Rent could not be updated for ID: {id}");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[RentController] Error occurred while updating rent.");
            return StatusCode(500, "Internal server error while updating rent.");
        }
    }

    // Delete a rent
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteRent(int id)
    {
        try
        {
            var rent = await _listingRepository.GetRentById(id);
            if (rent == null)
            {
                _logger.LogError($"[RentController] Rent not found for ID: {id}");
                return NotFound($"Rent not found for ID: {id}");
            }

            await _listingRepository.DeleteRent(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[RentController] Error occurred while deleting rent.");
            return StatusCode(500, "Internal server error while deleting rent.");
        }
    }
}