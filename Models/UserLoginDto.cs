using Microsoft.AspNetCore.Mvc;
using LosCasaAngular.Models;
using LosCasaAngular.DAL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace LosCasaAngular.Models
{
    public class UserLoginDto
    {
        public string Email { get; set; } = string.Empty; // Initializes to an empty string
        public string Password { get; set; } = string.Empty; // Initializes to an empty string
    }
}
