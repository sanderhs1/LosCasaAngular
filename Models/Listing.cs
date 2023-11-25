using System.Text.Json.Serialization;

namespace LosCasaAngular.Models;

public class Listing
{
    [JsonPropertyName("ListingId")]
    public int ListingId { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("Price")]
    public decimal Price { get; set; }

    [JsonPropertyName("Description")]
    public string? Description { get; set; }

    [JsonPropertyName("ImageUrl")]
    public string? ImageUrl { get; set; }
}
