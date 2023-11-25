using Castle.Core.Resource;
using System.Text.Json.Serialization;

namespace LosCasaAngular.Models;

public class Rent
{
    public int ListingId { get; set; }
    public int RentId { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
}
