using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LosCasaAngular.Models;

    public class Listing
    {
        [JsonPropertyName("ListingId")]
        public int ListingId { get; set; }

        [JsonPropertyName("AntallRom")]
        //[Range(1, 100, ErrorMessage = "Number of rooms should be between 1 and 100.")]
        public int AntallRom { get; set; }

        [JsonPropertyName("Bad")]
        public int Bad { get; set; }

        [JsonPropertyName("Area")]
        public int Area { get; set; }

        [JsonPropertyName("Floor")]
        public int Floor { get; set; }

        [JsonPropertyName("Spots")]
        public int Spots { get; set; }

        [JsonPropertyName("Adresse")]
        public string? Adresse { get; set; }


        [JsonPropertyName("Name")]
        //[RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,20}", ErrorMessage = "The Name must be numbers or letters and between 2 to 20 characters.")]
        //[Display(Name = "Item name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("Price")]
        //[Range(0.01, double.MaxValue, ErrorMessage = "The Price must be greater than 0. ")]
        public decimal Price { get; set; }

        [JsonPropertyName("Description")]
        //[StringLength(300)]
        public string? Description { get; set; }

        [JsonPropertyName("ImageUrl")]
        public string? ImageUrl { get; set; }

        //[JsonPropertyName("ImageUrl1")]
        //public string? ImageUrl1 { get; set; }

        //[JsonPropertyName("RentListings")]
    //    public virtual List<RentListing>? RentListings { get; set; }
    }

