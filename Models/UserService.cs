using LosCasaAngular.Controllers;
using LosCasaAngular.Models;
using Microsoft.AspNetCore.Identity;

namespace LosCasaAngular.Models
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager; // ASP.NET Core Identity UserManager
        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, password);
                return result;
            }
            return false;
        }

        public async Task<bool> Register(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                // Here you might want to sign in the user or even confirm their email before sign in
                return true;
            }
            else
            {
                // Handle the failure case, possibly logging the errors or throwing an exception
                return false;
            }
        }
    }
}
