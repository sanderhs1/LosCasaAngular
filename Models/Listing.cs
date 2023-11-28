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

    [JsonPropertyName("AdditionalImageUrl")]
    public string? AdditionalImageUrl { get; set; }

    [JsonPropertyName("Rooms")]
    public int Rooms { get; set; }

    [JsonPropertyName("Toilets")]
    public int Toilets { get; set; }

    [JsonPropertyName("Area")]
    public int Area { get; set; }

    [JsonPropertyName("Address")]
    public string? Address { get; set; }

}
